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
using CBD.Model.CategorySource;

namespace CBD.BAL.Managers
{
    public class CategorySourceManager : ICategorySourceManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(CATEGORYSOURCEParams model)
        {
            var result = new PagingResult();            
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var totalRecords = 0;

                    var dataList = unitOfWork.CategorySourceRepository.Search(model, out totalRecords);
                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search Category Sources unsuccessfully!";
                        return result;
                    }

                    result.Total = totalRecords;    
                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search Category Sources successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search Category Sources unsuccessfully!";
                return result;
            }
            return result;
        }

        public Result Add(INDUSTRY_CATEGORY_SOURCES model)
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
                    var data = unitOfWork.CategorySourceRepository.FirstOrDefault(x => x.CATEGORY_SOURCE_ID == model.CATEGORY_SOURCE_ID);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Category Source already existed. Please check it again!");
                        return result;
                    }

                    //data = unitOfWork.CategorySourceRepository.FirstOrDefault(x => x.CODE.Equals(model.CODE));
                    //if (data != null)
                    //{
                    //    result.Code = (short)HttpStatusCode.Conflict;
                    //    result.Message = string.Format("The Category Source's Code already existed. Please check it again!");
                    //    return result;
                    //}

                    data = new TBL_INDUSTRY_CATEGORY_SOURCES();
                    data.CATEGORY_ID = model.CATEGORY_ID;
                    data.SOURCE_ID = model.SOURCE_ID;
                    data.URL = model.URL;
                    data.KEY_SELECT = model.KEY_SELECT;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.CREATED_DATE = DateTime.Now;
                    data.CREATED_BY = model.CREATED_BY;

                    unitOfWork.CategorySourceRepository.Add(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Source Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Source Added unsuccessfully!");
                return result;
            }
        }

        public Result Update(INDUSTRY_CATEGORY_SOURCES model)
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
                    var data = unitOfWork.CategorySourceRepository.FirstOrDefault(x => x.CATEGORY_SOURCE_ID == model.CATEGORY_SOURCE_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Category Source did not find. Please check it again!");
                        return result;
                    }

                    data.CATEGORY_ID = model.CATEGORY_ID;
                    data.SOURCE_ID = model.SOURCE_ID;
                    data.URL = model.URL;
                    data.KEY_SELECT = model.KEY_SELECT;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.MODIFIED_DATE = DateTime.Now;
                    data.MODIFIED_BY = model.MODIFIED_BY;

                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Source Updated successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Source Updated unsuccessfully!");
                return result;
            }
        }

        public Result Delete(INDUSTRY_CATEGORY_SOURCES model)
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
                    var data = unitOfWork.CategorySourceRepository.GetById(model.CATEGORY_SOURCE_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Category Source did not find. Please check it again!");
                        return result;
                    }

                    unitOfWork.CategorySourceRepository.Delete(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Source Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Source Deleted unsuccessfully!");
                return result;
            }
        }

        public Result GetCategoryBySource(int? sourceID, int? pcategoryid)
        {
            var result = new Result();
            try
            {
                if (sourceID == null)
                {
                    result.Code = (short)HttpStatusCode.BadRequest;
                    result.Message = "The sourceID is null. Please check again!";
                    return result;
                }
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.CategorySourceRepository.GetCategoryBySource(sourceID, pcategoryid);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The get Category by Source did not find. Please check it again!");
                        return result;
                    }
                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = data;
                    result.Message = string.Format("The get Category by Source successfully!");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The get Category by Source unsuccessfully!");
                return result;
            }
        }

        #region Private Method
        #endregion
    }
}
