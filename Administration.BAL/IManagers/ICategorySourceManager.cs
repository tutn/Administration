using CBD.Model;
using CBD.Model.Common;
using CBD.Model.CategorySource;

namespace CBD.BAL.Managers
{
    public interface ICategorySourceManager
    {
        PagingResult Search(CATEGORYSOURCEParams model);
        Result Add(INDUSTRY_CATEGORY_SOURCES model);
        Result Update(INDUSTRY_CATEGORY_SOURCES model);
        Result Delete(INDUSTRY_CATEGORY_SOURCES model);
        Result GetCategoryBySource(int? sourceID, int? pcategoryid);
    }
}
