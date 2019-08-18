using CBD.Model;
using CBD.Model.CategorySourceDetail;
using CBD.Model.Common;

namespace CBD.BAL.Managers
{
    public interface ICategorySourceDetailManager
    {
        PagingResult Search(CATEGORYSOURCEDETAILParams model);
        Result Add(INDUSTRY_CATEGORY_SOURCE_DETAILS model);
        Result Update(INDUSTRY_CATEGORY_SOURCE_DETAILS model);
        Result Delete(INDUSTRY_CATEGORY_SOURCE_DETAILS model);
        Result GetLastestRecord(long? Id, int? CategoryId);
    }
}
