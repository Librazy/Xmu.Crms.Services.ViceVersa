using System.Collections.Generic;
using Xmu.Crms.Shared.Models;

namespace Xmu.Crms.Services.ViceVersa
{
    interface IClassDao
    {
        long Save(ClassInfo t);
        long InsertSelection(CourseSelection t);
        void Delete(long id);
        void DeleteSelection(long userId,long classId);
        int Update(ClassInfo t);
        List<ClassInfo> QueryAll(long id);
        ClassInfo Get(long id);
    }
}
