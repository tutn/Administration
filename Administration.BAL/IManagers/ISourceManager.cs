using CBD.Model;
using CBD.Model.Common;
using CBD.Model.Source;

namespace CBD.BAL.Managers
{
    public interface ISourceManager
    {
        PagingResult Search(SOURCEParams model);
        Result Add(INDUSTRY_SOURCES model);
        Result Update(INDUSTRY_SOURCES model);
        Result Delete(INDUSTRY_SOURCES model);
        Result GetAllSources();
    }
}
