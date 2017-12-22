using System.Collections.Generic;
using Xmu.Crms.Shared.Models;

namespace Xmu.Crms.Services.ViceVersa
{
    interface IClassDao
    {
        long Save(ClassInfo t);
        void Delete(long id);
        int Update(ClassInfo t);
        List<ClassInfo> QueryAll(long id);
        ClassInfo Get(long id);
    }
}
