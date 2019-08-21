using Administration.Model;
using Administration.Model.Common;

namespace Administration.BAL.IManagers
{
    public interface IParameterManager
    {
        PagingResult Search(PARAMETER_Params model);
        Result Add(SYS_PARAMETERS model);
        Result Update(SYS_PARAMETERS model);
        Result Delete(SYS_PARAMETERS model);
    }
}
