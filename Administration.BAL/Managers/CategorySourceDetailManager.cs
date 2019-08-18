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
using CBD.Model.CategorySourceDetail;

namespace CBD.BAL.Managers
{
    public class CategorySourceDetailManager : ICategorySourceDetailManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(CATEGORYSOURCEDETAILParams model)
        {
            var result = new PagingResult();
            var skipRecord = model.PageSize * model.PageNumber;
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.CategorySourceDetailRepository.QueryAll();
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search categories unsuccessfully!";
                        return result;
                    }

                    result.Total = data.Count();
                    var dataList = data.Skip(skipRecord).Take(model.PageSize).ToList();

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search categories successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search categories unsuccessfully!";
                return result;
            }
            return result;
        }

        public Result Add(INDUSTRY_CATEGORY_SOURCE_DETAILS model)
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
                    var data = unitOfWork.CategorySourceDetailRepository.FirstOrDefault(x => x.CATEGORY_SOURCE_DETAILS_ID == model.CATEGORY_SOURCE_DETAILS_ID);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Category Link already existed. Please check it again!");
                        return result;
                    }

                    data = unitOfWork.CategorySourceDetailRepository.FirstOrDefault(x => x.CODE == model.CODE);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Category Link's Code already existed. Please check it again!");
                        return result;
                    }

                    data = new TBL_INDUSTRY_CATEGORY_SOURCE_DETAILS();
                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.CATEGORY_SOURCE_ID = model.CATEGORY_SOURCE_ID;
                    data.URL = model.URL;
                    data.KEY_SELECT = model.KEY_SELECT;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.CREATED_DATE = DateTime.Now;
                    data.CREATED_BY = model.CREATED_BY;

                    unitOfWork.CategorySourceDetailRepository.Add(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Link Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Link Added unsuccessfully!");
                return result;
            }
        }

        public Result Update(INDUSTRY_CATEGORY_SOURCE_DETAILS model)
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
                    var data = unitOfWork.CategorySourceDetailRepository.FirstOrDefault(x => x.CATEGORY_SOURCE_DETAILS_ID == model.CATEGORY_SOURCE_DETAILS_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Category Link did not find. Please check it again!");
                        return result;
                    }

                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.CATEGORY_SOURCE_ID = model.CATEGORY_SOURCE_ID;
                    data.URL = model.URL;
                    data.KEY_SELECT = model.KEY_SELECT;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.MODIFIED_DATE = DateTime.Now;
                    data.MODIFIED_BY = model.MODIFIED_BY;

                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Link Updated successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Link Updated unsuccessfully!");
                return result;
            }
        }

        public Result Delete(INDUSTRY_CATEGORY_SOURCE_DETAILS model)
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
                    var data = unitOfWork.CategorySourceDetailRepository.GetById(model.CATEGORY_SOURCE_DETAILS_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Category Link did not find. Please check it again!");
                        return result;
                    }

                    unitOfWork.CategorySourceDetailRepository.Delete(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Link Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Link Deleted unsuccessfully!");
                return result;
            }
        }

        public Result GetLastestRecord(long? Id, int? CategoryId)
        {
            var result = new Result();
            try
            {
                var data = new INDUSTRY_CATEGORY_SOURCE_DETAILS();
                if (Id == null || CategoryId == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The ID is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var item = unitOfWork.CategorySourceDetailRepository.Where(x => x.CATEGORY_SOURCE_ID == CategoryId && x.URL_ID == Id).OrderByDescending(o => o.CREATED_DATE).FirstOrDefault();
                    if (item == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Category Link did not find. Please check it again!");
                        return result;
                    }
                    data.CODE = item.CODE;
                    data.NAME = item.NAME;
                    data.CATEGORY_SOURCE_ID = item.CATEGORY_SOURCE_ID;
                    data.URL = item.URL;
                    data.KEY_SELECT = item.KEY_SELECT;
                    data.USED_STATE = item.USED_STATE;
                    data.DESCRIPTION = item.DESCRIPTION;
                    data.MODIFIED_DATE = item.CREATED_DATE;
                    data.MODIFIED_BY = item.MODIFIED_BY;
                    data.MODIFIED_DATE = item.MODIFIED_DATE;
                    data.MODIFIED_BY = item.MODIFIED_BY;
                }

                result.Code = (short)HttpStatusCode.OK;
                result.Data = data;
                result.Message = string.Format("The Category Link Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Link Deleted unsuccessfully!");
                return result;
            }
        }
        #region Private Method
        #endregion
    }
}
