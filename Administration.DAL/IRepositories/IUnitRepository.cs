using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using System.Linq;

namespace Administration.DAL.Repositories
{
    public interface IUnitRepository : IRepository<TBL_SYS_UNITS, int>
    {
        IQueryable<SYS_UNITS> Search(UNIT_Params model);
        IQueryable<SYS_UNITS> GetAllUnits();
    }
}
