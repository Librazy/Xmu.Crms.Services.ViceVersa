using Microsoft.EntityFrameworkCore;
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
            try
            {
                StudentScoreGroup ssg = new StudentScoreGroup { Id = topicId };
                //将实体附加到对象管理器中
                _db.StudentScoreGroup.Attach(ssg);
                //删除
                _db.StudentScoreGroup.Remove(ssg);
                _db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public SeminarGroup GetSeminarGroupBySeminarGroupId(long seminarGroupId)
        {
            try
            {
                SeminarGroup group = _db.SeminarGroup.SingleOrDefault(c => c.Id == seminarGroupId);
                return group;
            }
            catch
            {
                throw;
            }
        }
        //不用写，调用其他的
        public List<SeminarGroup> ListSeminarGradeByCourseId(long userId, long courseId)
        {
            return null;
        }
        //先在seminarGroupTopic 和userinfo service查好，在这里传入实体对象seminarGroupTopic，userInfo
        public void InsertGroupGradeByUserId(SeminarGroupTopic seminarGroupTopic, UserInfo userInfo, int grade)
        {
            try
            {
                StudentScoreGroup ssg = new StudentScoreGroup { Student = userInfo, SeminarGroupTopic = seminarGroupTopic, Grade = grade };
                _db.StudentScoreGroup.Add(ssg);
                _db.SaveChanges();
            }
            catch
            {
                throw;
            }
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
            }
            catch
            {
                throw;
            }
        }

        //在SeminarGroupService调用 IList<SeminarGroup> ListSeminarGroupIdByStudentId(long userId);
        public List<SeminarGroup> ListSeminarGradeByStudentId(long userId)
        {
            return null;
        }
        public void Change(long[] idList, double[] gradeList, int i, int j)
        {
            long id = idList[i];
            double grade = gradeList[i];
            idList[i] = idList[j];
            gradeList[i] = gradeList[j];
            idList[j] = id;
            gradeList[j] = grade;
        }
        public void QuickSort(long[] idList, double[] gradeList, int low, int high)
        {
            if (low >= high) return;
            Random ran = new Random();
            int flag = ran.Next(low, high);
            long id = idList[flag];
            double grade = gradeList[flag];
            int i = low;
            int j = high;
            while (i < j)
            {
                while (gradeList[j] <= grade && i < j) j--;
                if (i < j)
                {
                    Change(idList, gradeList, i, j);
                    i++;
                }
                while (gradeList[j] >= grade && i < j) i++;
                if (i < j)
                {
                    Change(idList, gradeList, i, j);
                    j--;
                }
            }
            gradeList[i] = grade;
            idList[i] = id;
            QuickSort(idList, gradeList, low, i - 1);
            QuickSort(idList, gradeList, i + 1, high);
        }
        public void CalculatePreGradeByTopicId(long seminarId, IList<Topic> topicList)
        {
            foreach (var topic in topicList)
            {
                //通过seminarGroupId获得List<SeminarGroupTopic>  
                //通过seminarGrouptopicId获得List<StudentScoreGroup>
                long[] idList;
                double[] gradeList;

                //获取选择该topic的所有小组
                var seminarGroupTopicList = _db.SeminarGroupTopic.Include(u => u.SeminarGroup).Include(u => u.Topic).Where(u => u.Topic.Id == topic.Id).ToList();
                if (seminarGroupTopicList != null)
                {
                    idList = new long[seminarGroupTopicList.Count];
                    gradeList = new double[seminarGroupTopicList.Count];

                    int groupNumber = 0;

                    //计算一个小组的所有学生打分情况
                    foreach (var i in seminarGroupTopicList)
                    {
                        //List<StudentScoreGroup> studentScoreList = new List<StudentScoreGroup>();
                        //获取学生打分列表
                        var studentScoreList = _db.StudentScoreGroup.Where(u => u.SeminarGroupTopic.Id == i.Id).ToList();
                        double grade = 0; int k = 0;
                        foreach (var g in studentScoreList)
                        {
                            //grade += g.Grade;
                            k++;
                        }
                        double avg = (double)grade / k;

                        //将小组该讨论课平均分和Id保存
                        idList[groupNumber] = i.Id;
                        gradeList[groupNumber] = avg;
                    }
                    //将小组成绩从大到小排序
                    QuickSort(idList, gradeList, 0, groupNumber);

                    Seminar seminar;
                    ClassInfo classInfo;
                    try
                    {
                        seminar = _db.Seminar.Include(u => u.Course).Where(u => u.Id == seminarId).SingleOrDefault();
                        classInfo = _db.ClassInfo.Where(u => u.Id == seminar.Course.Id).SingleOrDefault();
                    }
                    catch
                    {
                        throw;
                    }
                    //各小组按比例给分
                    int A = Convert.ToInt32(groupNumber * classInfo.FivePointPercentage * 0.01);
                    int B = Convert.ToInt32(groupNumber * classInfo.FourPointPercentage * 0.01);
                    int C = Convert.ToInt32(groupNumber * classInfo.ThreePointPercentage * 0.01);
                    for (int i = 0; i < A; i++)
                    {
                        try
                        {
                            SeminarGroupTopic seminarGroupTopic = _db.SeminarGroupTopic.SingleOrDefault(s => s.Id == idList[i]);
                            //如果找不到该组
                            if (seminarGroupTopic == null)
                            {
                                throw new GroupNotFoundException();
                            }
                            //更新报告分
                            seminarGroupTopic.PresentationGrade = 5;
                            _db.SaveChanges();
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    for (int i = A; i < B; i++)
                    {
                        try
                        {
                            SeminarGroupTopic seminarGroupTopic = _db.SeminarGroupTopic.SingleOrDefault(s => s.Id == idList[i]);
                            //如果找不到该组
                            if (seminarGroupTopic == null)
                            {
                                throw new GroupNotFoundException();
                            }
                            //更新报告分
                            seminarGroupTopic.PresentationGrade = 4;
                            _db.SaveChanges();
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    for (int i = B; i <= groupNumber; i++)
                    {
                        try
                        {
                            SeminarGroupTopic seminarGroupTopic = _db.SeminarGroupTopic.SingleOrDefault(s => s.Id == idList[i]);
                            //如果找不到该组
                            if (seminarGroupTopic == null)
                            {
                                throw new GroupNotFoundException();
                            }
                            //更新报告分
                            seminarGroupTopic.PresentationGrade = 3;
                            _db.SaveChanges();
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }//if end

            }//foreach topic end
        }

        //只需要讨论课ID，因为要排序，必须要一起计算
        public void CountPresentationGrade(long seminarId, IList<Topic> topicList)
        {
            CalculatePreGradeByTopicId(seminarId, topicList);//计算每个小组每个topic得分

            //通过seminarGrouptopicId获得List<StudentScoreGroup>
        }

        //只需要讨论课ID，同上
        public void CountGroupGradeBySerminarId(long seminarId, long seminarGroupId)
        {

        }
    }
}
