using log4net;
using System.Reflection;
using System;
using System.Net;
using CBD.Model.Common;
using CBD.DAL.Common;
using CBD.Model;
using CBD.DAL.Entities;
using CBD.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using CBD.Model.Configuration;
using CBD.Model.Utilities;
using CBD.Model.Source;

namespace CBD.BAL.Managers
{
    public class SourceManager : ISourceManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(SOURCEParams model)
        {
            var result = new PagingResult();            
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var totalRecords = 0;
                    var skipRecord = model.PageSize * model.PageNumber;

                    var data = unitOfWork.SourceRepository.QueryAll();
                    if (!string.IsNullOrWhiteSpace(model.CODE))
                    {
                        data = data.Where(x => x.CODE.Equals(model.CODE));
                    }
                    if (!string.IsNullOrWhiteSpace(model.NAME))
                    {
                        data = data.Where(x => x.NAME.Contains(model.NAME));
                    }
                    if (model.USED_STATE != null && model.USED_STATE > 0)
                    {
                        data = data.Where(x => x.USED_STATE.Equals(model.USED_STATE));
                    }

                    totalRecords = data.Count();

                    var dataList = data.OrderBy(o => o.CREATED_DATE).Skip(skipRecord).Take(model.PageSize).AsEnumerable().Select(s=>new INDUSTRY_SOURCES {
                        SOURCE_ID = s.SOURCE_ID,
                        CODE = s.CODE,
                        NAME = s.NAME,
                        URL = s.URL,
                        USED_STATE = s.USED_STATE,
                        USEDSTATE_NAME = s.USED_STATE != null && s.USED_STATE > 0 ? Enums.Description((USED_STATE)s.USED_STATE) : string.Empty,
                        DESCRIPTION = s.DESCRIPTION,
                        CREATED_BY = s.CREATED_BY,
                        CREATED_DATE = s.CREATED_DATE,
                        MODIFIED_BY = s.MODIFIED_BY,
                        MODIFIED_DATE = s.MODIFIED_DATE,
                    }).ToList();
                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search Sources unsuccessfully!";
                        return result;
                    }

                    result.Total = totalRecords;    
                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search Sources successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search Sources unsuccessfully!";
                return result;
            }
            return result;
        }

        public Result Add(INDUSTRY_SOURCES model)
        {
            var result = new Result();
            try
            {
                if (model == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The model is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.SourceRepository.FirstOrDefault(x => x.SOURCE_ID == model.SOURCE_ID);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Source already existed. Please check it again!");
                        return result;
                    }

                    data = unitOfWork.SourceRepository.FirstOrDefault(x => x.CODE.Equals(model.CODE));
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Source's Code already existed. Please check it again!");
                        return result;
                    }

                    data = new TBL_INDUSTRY_SOURCES();
                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.URL = model.URL;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.CREATED_DATE = DateTime.Now;
                    data.CREATED_BY = model.CREATED_BY;

                    unitOfWork.SourceRepository.Add(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Source Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Source Added unsuccessfully!");
                return result;
            }
        }

        public Result Update(INDUSTRY_SOURCES model)
        {
            var result = new Result();
            try
            {
                if (model == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The model is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.SourceRepository.FirstOrDefault(x => x.SOURCE_ID == model.SOURCE_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Source did not find. Please check it again!");
                        return result;
                    }

                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.URL = model.URL;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.MODIFIED_DATE = DateTime.Now;
                    data.MODIFIED_BY = model.MODIFIED_BY;

                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Source Updated successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Source Updated unsuccessfully!");
                return result;
            }
        }

        public Result Delete(INDUSTRY_SOURCES model)
        {
            var result = new Result();
            try
            {
                if (model == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The ID is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.SourceRepository.GetById(model.SOURCE_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Source did not find. Please check it again!");
                        return result;
                    }

                    unitOfWork.SourceRepository.Delete(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Source Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Source Deleted unsuccessfully!");
                return result;
            }
        }

        public Result GetAllSources()
        {
            var result = new Result();
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var dataList = unitOfWork.SourceRepository.QueryAll().AsEnumerable().Select(s => new INDUSTRY_SOURCES
                    {
                        SOURCE_ID = s.SOURCE_ID,
                        CODE = s.CODE,
                        NAME = s.NAME,
                        URL = s.URL,
                        USED_STATE = s.USED_STATE,
                        USEDSTATE_NAME = s.USED_STATE != null && s.USED_STATE > 0 ? Enums.Description((USED_STATE)s.USED_STATE) : string.Empty,
                        DESCRIPTION = s.DESCRIPTION,
                        CREATED_BY = s.CREATED_BY,
                        CREATED_DATE = s.CREATED_DATE,
                        MODIFIED_BY = s.MODIFIED_BY,
                        MODIFIED_DATE = s.MODIFIED_DATE,
                    }).ToList();

                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Source did not find. Please check it again!");
                        return result;
                    }

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = string.Format("The Source Deleted successfully!");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Source Deleted unsuccessfully!");
                return result;
            }
        }

        #region Private Method
        #endregion
    }
}
