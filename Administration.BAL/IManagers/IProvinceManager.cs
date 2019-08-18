using CBD.Model;
using CBD.Model.Common;
using CBD.Model.Province;

namespace CBD.BAL.Managers
{
    public interface IProvinceManager
    {
        PagingResult Search(PROVINCEParams model);
        Result Add(SYS_PROVINCES model);
        Result Update(SYS_PROVINCES model);
        Result Delete(SYS_PROVINCES model);
        Result GetAllProvinces(int? ProvinceId);
    }
}
