﻿using Administration.DAL.Common;
using Administration.DAL.Entities;
using Administration.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Administration.Model.Enums;

namespace Administration.DAL.Repositories
{
    public class UserRepository : Repository<TBL_SYS_USERS, int>, IUserRepository
    {
        private Repository<TBL_SYS_USERS, int> _hrRepository;
        private DbSet<TBL_SYS_USERS> _dbSet;
        private readonly AdminDbContext _dbContext;

        public UserRepository(AdminDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _hrRepository = new Repository<TBL_SYS_USERS, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_USERS>();
        }

        public List<SYS_USERS> Search(USER_Params model, out int totalRecords)
        {
            var skipRecord = model.PageSize * model.PageNumber;
            var query = (from u in _dbContext.TBL_SYS_USERS
                         where (model.USER_NAME == null || model.USER_NAME == "" || u.USER_NAME.Contains(model.USER_NAME))
                         && (model.FULL_NAME == null || model.FULL_NAME == "" || u.FULL_NAME.Contains(model.FULL_NAME))
                         && (model.EMAIL == null || model.EMAIL == "" || u.EMAIL.Contains(model.EMAIL))
                         && (model.USED_STATE == null || model.USED_STATE == 0 || u.USED_STATE == model.USED_STATE)
                         select u);
            totalRecords = query.Count();
            var data = query.OrderBy(o => o.USER_NAME).Skip(skipRecord).Take(model.PageSize).AsEnumerable();
            var dataList = data != null && totalRecords > 0 ? data.Select(s => new SYS_USERS
            {
                USER_ID = s.USER_ID,
                USER_NAME = s.USER_NAME,
                PASSWORD = s.PASSWORD,
                FULL_NAME = s.FULL_NAME,
                EMAIL = s.EMAIL,
                AVATAR = s.AVATAR,
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
