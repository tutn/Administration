using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using Administration.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.DAL.Repositories
{
    public interface IPageRepository : IRepository<TBL_SYS_PAGES, int>
    {
        IQueryable<SYS_PAGES> Search(PAGEParams model);
        IQueryable<SYS_PAGES> GetAllPages();
    }
}
