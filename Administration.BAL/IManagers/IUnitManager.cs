using Administration.Model;
using Administration.Model.Common;

namespace Administration.BAL.IManagers
{
    public interface IUnitManager
    {
        PagingResult Search(UNIT_Params model);
        Result Add(SYS_UNITS model);
        Result Update(SYS_UNITS model);
        Result Delete(SYS_UNITS model);
        Result GetAllUnits(int? UnitId);
        Result GetUnitNodes();
    }
}
