using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using Administration.Model.Province;
using System.Collections.Generic;
using System.Linq;

namespace Administration.DAL.Repositories
{
    public interface IProvinceRepository : IRepository<TBL_SYS_PROVINCES, int>
    {
        List<SYS_PROVINCES> Search(PROVINCEParams model, out int totalRecord);
        List<SYS_PROVINCES> GetAllProvinces();
    }
}
