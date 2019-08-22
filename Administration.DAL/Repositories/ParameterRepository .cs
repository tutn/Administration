using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Administration.Model.Enums;

namespace Administration.DAL.Repositories
{
    public class ParameterRepository : Repository<TBL_SYS_PARAMETERS, int>, IParameterRepository
    {
        private readonly Repository<TBL_SYS_PARAMETERS, int> _repository;
        private readonly DbSet<TBL_SYS_PARAMETERS> _dbSet;
        private readonly AdminDbContext _dbContext;

        public ParameterRepository(AdminDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = new Repository<TBL_SYS_PARAMETERS, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_PARAMETERS>();
        }

        public List<SYS_PARAMETERS> Search(PARAMETER_Params model, out int totalRecords)
        {
            var skipRecord = model.PageSize * model.PageNumber;
            var query = (from u in _dbContext.TBL_SYS_PARAMETERS
                         where (model.TYPE == null || model.TYPE == "" || u.TYPE.Contains(model.TYPE))
                         && (model.NAME == null || model.NAME == "" || u.NAME.Contains(model.NAME))
                         && (model.USED_STATE == null || model.USED_STATE == 0 || u.USED_STATE == model.USED_STATE)
                         select u);
            totalRecords = query.Count();
            var data = query.AsEnumerable().Skip(skipRecord).Take(model.PageSize);
            var dataList = data != null && totalRecords > 0 ? data.Select(s => new SYS_PARAMETERS
            {
                ID = s.ID,
                TYPE = s.TYPE,
                NAME = s.NAME,
                VALUE = s.VALUE,
                ORDER_NO = s.ORDER_NO,
                USED_STATE = s.USED_STATE,
                USEDSTATE_NAME = s.USED_STATE != null && s.USED_STATE > 0 ? Enums.Description((USED_STATE)s.USED_STATE) : string.Empty,
                DESCRIPTION = s.DESCRIPTION,
                CREATED_DATE = s.CREATED_DATE,
                CREATED_BY = s.CREATED_BY,
                MODIFIED_DATE = s.MODIFIED_DATE,
                MODIFIED_BY = s.MODIFIED_BY,
            }).ToList() : null;
            return dataList;
        }


        #region Private Method
        #endregion
    }
}
