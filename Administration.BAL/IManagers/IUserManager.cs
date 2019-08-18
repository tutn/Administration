using Administration.Model;
using Administration.Model.Common;

namespace Administration.BAL.IManagers
{
    public interface IUserManager
    {
        PagingResult Search(USER_Params model);
        Result Add(SYS_USERS model);
        Result Update(SYS_USERS model);
        Result Delete(SYS_USERS model);
    }
}
