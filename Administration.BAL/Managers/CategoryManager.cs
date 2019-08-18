using log4net;
using System.Reflection;
using System;
using System.Net;
using CBD.Model.Common;
using CBD.Model.Unit;
using CBD.DAL.Common;
using CBD.Model;
using CBD.DAL.Entities;
using CBD.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using CBD.Model.Configuration;
using CBD.Model.Category;

namespace CBD.BAL.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(CATEGORYParams model)
        {
            var result = new PagingResult();
            var skipRecord = model.PageSize * model.PageNumber;
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var data = unitOfWork.CategoryRepository.Search(model);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search categories unsuccessfully!";
                        return result;
                    }

                    result.Total = data.Count();
                    var dataList = GetChildren(data, model.PARENT_ID, SystemConfiguration.PREFIXC, string.Empty, null, true);
                    dataList = dataList.Skip(skipRecord).Take(model.PageSize).ToList();

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

        public Result Add(INDUSTRY_CATEGORIES model)
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
                    var data = unitOfWork.CategoryRepository.FirstOrDefault(x => x.CATEGORY_ID == model.CATEGORY_ID);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Category already existed. Please check it again!");
                        return result;
                    }

                    data = unitOfWork.CategoryRepository.FirstOrDefault(x => x.CODE == model.CODE);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Category's Code already existed. Please check it again!");
                        return result;
                    }

                    data = new TBL_INDUSTRY_CATEGORIES();
                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.PARENT_ID = model.PARENT_ID;
                    data.LEVEL = model.LEVEL;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.CREATED_DATE = DateTime.Now;
                    data.CREATED_BY = model.CREATED_BY;

                    unitOfWork.CategoryRepository.Add(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Added unsuccessfully!");
                return result;
            }
        }

        public Result Update(INDUSTRY_CATEGORIES model)
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
                    var data = unitOfWork.CategoryRepository.FirstOrDefault(x => x.CATEGORY_ID == model.CATEGORY_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Category did not find. Please check it again!");
                        return result;
                    }

                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.PARENT_ID = model.PARENT_ID;
                    data.LEVEL = model.LEVEL;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.MODIFIED_DATE = DateTime.Now;
                    data.MODIFIED_BY = model.MODIFIED_BY;

                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Updated successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Updated unsuccessfully!");
                return result;
            }
        }

        public Result Delete(INDUSTRY_CATEGORIES model)
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
                    var data = unitOfWork.CategoryRepository.GetById(model.CATEGORY_ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Category did not find. Please check it again!");
                        return result;
                    }

                    unitOfWork.CategoryRepository.Delete(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Category Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Category Deleted unsuccessfully!");
                return result;
            }
        }

        public Result GetAllCategories(int? CategoryId)
        {
            var result = new Result();
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {

                    var data = unitOfWork.CategoryRepository.GetAllCategories();
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "The category did not found. Please check again!";
                        return result;
                    }

                    var dataList = new List<INDUSTRY_CATEGORIES>();
                    if (CategoryId != null && CategoryId != 0)
                    {
                        var subdata = GetChildren(data, CategoryId, string.Empty, string.Empty, null, false);
                        var subIds = subdata.Select(s => s.CATEGORY_ID).ToList();
                        subIds.Add(CategoryId.Value);
                        var newdata = data.Where(x => !subIds.Contains(x.CATEGORY_ID)).ToList();
                        dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, subIds, false);
                    }
                    else
                    {
                        dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, null, false);
                    }

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Get all menu successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Get all menu unsuccessfully!";
                return result;
            }
            return result;
        }
        
        #region Private Method
        private List<INDUSTRY_CATEGORIES> GetChildren(IQueryable<INDUSTRY_CATEGORIES> dataList, int? parentId, string prefixc, string parentprefix, List<int> disableIds, bool isSearch)
        {
            var dataLst = new List<INDUSTRY_CATEGORIES>();
            var prefixcharactor = string.Empty;
            var objs = dataList.Where(f => f.PARENT_ID == parentId).OrderBy(o => o.CATEGORY_ID);
            
            foreach (var item in objs)
            {
                if (disableIds == null || disableIds.Count == 0)
                {
                    item.IS_DISABLE = false;
                }
                else
                {
                    if (disableIds.Contains(item.CATEGORY_ID))
                    {
                        item.IS_DISABLE = true;
                    }
                }
                
                if (isSearch)
                {
                    item.NAME = string.Format("{0} {1}", prefixcharactor, item.NAME);
                    item.USEDSTATE_NAME = item.USED_STATE != null && item.USED_STATE > 0 ? Enums.Description((USED_STATE)item.USED_STATE) : string.Empty;
                }
                else
                {
                    if (parentId != null)
                    {
                        prefixcharactor = string.Format("{0}{1}", prefixc, parentprefix);
                    }
                    item.NAME = string.Format("{0} {1}", prefixcharactor, item.NAME);
                }

                dataLst.Add(item);
                var subdata = GetChildren(dataList, item.CATEGORY_ID, prefixc, prefixcharactor, disableIds, isSearch);
                dataLst.AddRange(subdata);
            }

            return dataLst;
        }

        #endregion
    }
}
