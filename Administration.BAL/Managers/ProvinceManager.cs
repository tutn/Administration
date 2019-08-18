﻿using log4net;
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
using CBD.Model.Province;

namespace CBD.BAL.Managers
{
    public class ProvinceManager : IProvinceManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PagingResult Search(PROVINCEParams model)
        {
            var result = new PagingResult();
            var totalRecord = 0;
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    var dataList = unitOfWork.ProvinceRepository.Search(model, out totalRecord);
                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "Search provinces unsuccessfully!";
                        return result;
                    }

                    result.Total = totalRecord;
                    //var dataList = GetChildren(data, model.PARENT_ID, SystemConfiguration.PREFIXC, string.Empty, null, true);
                    //dataList = dataList.Skip(skipRecord).Take(model.PageSize).ToList();

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Search provinces successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Search provinces unsuccessfully!";
                return result;
            }
            return result;
        }

        public Result Add(SYS_PROVINCES model)
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
                    var data = unitOfWork.ProvinceRepository.FirstOrDefault(x => x.ID == model.ID);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Province already existed. Please check it again!");
                        return result;
                    }

                    data = unitOfWork.ProvinceRepository.FirstOrDefault(x => x.CODE == model.CODE);
                    if (data != null)
                    {
                        result.Code = (short)HttpStatusCode.Conflict;
                        result.Message = string.Format("The Province's Code already existed. Please check it again!");
                        return result;
                    }

                    data = new TBL_SYS_PROVINCES();
                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.PARENT_ID = model.PARENT_ID;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.CREATED_DATE = DateTime.Now;
                    data.CREATED_BY = model.CREATED_BY;

                    unitOfWork.ProvinceRepository.Add(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Province Added successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Province Added unsuccessfully!");
                return result;
            }
        }

        public Result Update(SYS_PROVINCES model)
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
                    var data = unitOfWork.ProvinceRepository.FirstOrDefault(x => x.ID == model.ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Province did not find. Please check it again!");
                        return result;
                    }

                    data.CODE = model.CODE;
                    data.NAME = model.NAME;
                    data.PARENT_ID = model.PARENT_ID;
                    data.USED_STATE = model.USED_STATE;
                    data.DESCRIPTION = model.DESCRIPTION;
                    data.MODIFIED_DATE = DateTime.Now;
                    data.MODIFIED_BY = model.MODIFIED_BY;

                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Province Updated successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Province Updated unsuccessfully!");
                return result;
            }
        }

        public Result Delete(SYS_PROVINCES model)
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
                    var data = unitOfWork.ProvinceRepository.GetById(model.ID);
                    if (data == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = string.Format("The Province did not find. Please check it again!");
                        return result;
                    }

                    unitOfWork.ProvinceRepository.Delete(data);
                    unitOfWork.SaveChanges();

                }

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("The Province Deleted successfully!");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = string.Format("The Province Deleted unsuccessfully!");
                return result;
            }
        }

        public Result GetAllProvinces(int? ProvinceId)
        {
            var result = new Result();
            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {

                    var dataList = unitOfWork.ProvinceRepository.GetAllProvinces();
                    if (dataList == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound;
                        result.Message = "The Province did not found. Please check again!";
                        return result;
                    }

                    //var dataList = new List<SYS_PROVINCES>();
                    //if (ProvinceId != null && ProvinceId != 0)
                    //{
                    //    var subdata = GetChildren(data, ProvinceId, string.Empty, string.Empty, null, false);
                    //    var subIds = subdata.Select(s => s.ID).ToList();
                    //    subIds.Add(ProvinceId.Value);
                    //    var newdata = data.Where(x => !subIds.Contains(x.ID)).ToList();
                    //    dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, subIds, false);
                    //}
                    //else
                    //{
                    //    dataList = GetChildren(data, null, SystemConfiguration.PREFIXC, string.Empty, null, false);
                    //}

                    result.Code = (short)HttpStatusCode.OK;
                    result.Data = dataList;
                    result.Message = "Get all Province successfully!";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = "Get all Province unsuccessfully!";
                return result;
            }
            return result;
        }

        #region Private Method
        private List<SYS_PROVINCES> GetChildren(IQueryable<SYS_PROVINCES> dataList, int? parentId, string prefixc, string parentprefix, List<int> disableIds, bool isSearch)
        {
            var dataLst = new List<SYS_PROVINCES>();
            var prefixcharactor = string.Empty;
            var objs = dataList.Where(f => f.PARENT_ID == parentId).OrderBy(o => o.ID);
            
            foreach (var item in objs)
            {
                if (disableIds == null || disableIds.Count == 0)
                {
                    item.IS_DISABLE = false;
                }
                else
                {
                    if (disableIds.Contains(item.ID))
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
                var subdata = GetChildren(dataList, item.ID, prefixc, prefixcharactor, disableIds, isSearch);
                dataLst.AddRange(subdata);
            }

            return dataLst;
        }
        #endregion
    }
}
