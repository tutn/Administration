using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using Administration.Model.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.DAL.Repositories
{
    public interface IGroupRepository : IRepository<TBL_SYS_GROUPS, int>
    {
        List<SYS_GROUPS> Search(GROUPParams model, out int totalRecords);
    }
}
