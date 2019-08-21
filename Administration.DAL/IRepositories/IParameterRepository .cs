using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using System.Collections.Generic;

namespace Administration.DAL.Repositories
{
    public interface IParameterRepository : IRepository<TBL_SYS_PARAMETERS, int>
    {
        List<SYS_PARAMETERS> Search(PARAMETER_Params model, out int totalRecords);
    }
}
