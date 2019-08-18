using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Administration.Model.Enums;

namespace Administration.DAL.Repositories
{
    public class ProvinceRepository : Repository<TBL_SYS_PROVINCES, int>, IProvinceRepository
    {
        private Repository<TBL_SYS_PROVINCES, int> _repository;
        private DbSet<TBL_SYS_PROVINCES> _dbSet;
        private readonly AdminDbContext _dbContext;

        public ProvinceRepository(AdminDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = new Repository<TBL_SYS_PROVINCES, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_PROVINCES>();
        }

        public List<SYS_PROVINCES> Search(PROVINCE_Params model, out int totalRecord)
        {
            var skipRecord = model.PageSize * model.PageNumber;
            var query = (from u in _dbContext.TBL_SYS_PROVINCES
                         from up in _dbContext.TBL_SYS_PROVINCES.Where(x => x.ID == u.PARENT_ID).DefaultIfEmpty()
                         where (model.CODE == null || model.CODE == "" || u.CODE.Contains(model.CODE))
                         && (model.NAME == null || model.NAME == "" || u.NAME.Contains(model.NAME))
                         && (model.PARENT_ID == null || model.PARENT_ID == 0 || u.PARENT_ID == model.PARENT_ID)
                         && (model.USED_STATE == null || model.USED_STATE == 0 || u.USED_STATE == model.USED_STATE)
                         && (model.TYPE == null || model.TYPE == 0 || u.TYPE == model.TYPE)
                         select new SYS_PROVINCES
                         {
                             ID = u.ID,
                             CODE = u.CODE,
                             NAME = u.NAME,
                             PARENT_ID = u.PARENT_ID,
                             PARENT_NAME = up.NAME,
                             USED_STATE = u.USED_STATE,
                             USEDSTATE_NAME = u.USED_STATE != null && u.USED_STATE > 0 ? Enums.Description((USED_STATE)u.USED_STATE) : string.Empty,
                             DESCRIPTION = u.DESCRIPTION,
                             CREATED_DATE = u.CREATED_DATE,
                             CREATED_BY = u.CREATED_BY,
                             MODIFIED_DATE = u.MODIFIED_DATE,
                             MODIFIED_BY = u.MODIFIED_BY,
                         });
            totalRecord = query.Count();
            var dataList = query.OrderBy(o => o.CODE).Skip(skipRecord).Take(model.PageSize).ToList();
            return dataList;
        }

        public List<SYS_PROVINCES> GetAllProvinces()
        {
            var query = (from p in _dbContext.TBL_SYS_PROVINCES
                         select new SYS_PROVINCES
                         {
                             ID = p.ID,
                             CODE = p.CODE,
                             NAME = p.NAME,
                             PARENT_ID = p.PARENT_ID,
                             USED_STATE = p.USED_STATE,
                             CREATED_DATE = p.CREATED_DATE,
                             CREATED_BY = p.CREATED_BY,
                             MODIFIED_DATE = p.MODIFIED_DATE,
                             MODIFIED_BY = p.MODIFIED_BY,
                         });
            return query.ToList();
        }

        #region Private Method
        #endregion
    }
}
