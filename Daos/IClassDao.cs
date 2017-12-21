using System.Collections.Generic;
using Xmu.Crms.Shared.Models;

namespace Xmu.Crms.Services.ViceVersa
{
    interface IClassDao
    {
        void Save(ClassInfo t);
        void Delete(long id);
        void Update(ClassInfo t);
        List<ClassInfo> QueryAll(long id);
        ClassInfo Get(long id);
    }
}
