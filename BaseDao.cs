using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xmu.Crms.Web.ViceVersa
{
    public interface BaseDao<T>
    {
         void Save(T t);
         void Delete(long id);
         void Update(T t);
         List<T> QueryAll();
         T Get(long id);
    }
}
