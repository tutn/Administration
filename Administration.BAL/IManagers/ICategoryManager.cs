using CBD.Model;
using CBD.Model.Category;
using CBD.Model.Common;

namespace CBD.BAL.Managers
{
    public interface ICategoryManager
    {
        PagingResult Search(CATEGORYParams model);
        Result Add(INDUSTRY_CATEGORIES model);
        Result Update(INDUSTRY_CATEGORIES model);
        Result Delete(INDUSTRY_CATEGORIES model);
        Result GetAllCategories(int? CategoryId);
    }
}
