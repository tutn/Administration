using Administration.DAL.Entities;
using Administration.DAL.Repositories;
using System;

namespace Administration.DAL.Common
{
    public interface IUnitOfWork : IDisposable
    {
        AdminDbContext DbContext { get; }
        #region System
        IRepository<TBL_SYS_DIMDATE, int> DimDateRepository { get; }
        IParameterRepository ParameterRepository { get; }
        //IPageRepository PageRepository { get; }
        IUnitRepository UnitRepository { get; }
        IUserRepository UserRepository { get; }
        //IGroupRepository GroupRepository { get; }
        //IUnitUserRepository UnitUserRepository { get; }
        //IRepository<TBL_SYS_GROUP_USERS, int> GroupUserRepository { get; }
        //IUnitGroupPageRepository UnitGroupPageRepository { get; }
        //ICategoryRepository CategoryRepository { get; }
        //IProvinceRepository ProvinceRepository { get; }
        //ICategorySourceRepository CategorySourceRepository { get; }

        #endregion

        void SaveChanges();
    }
}