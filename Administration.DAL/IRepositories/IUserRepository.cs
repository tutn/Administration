using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using System.Collections.Generic;

namespace Administration.DAL.Repositories
{
    public interface IUserRepository : IRepository<TBL_SYS_USERS, int>
    {
        List<SYS_USERS> Search(USER_Params model, out int totalRecords);
    }
}
