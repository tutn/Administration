
using Administration.BAL.IManagers;
using Administration.BAL.Managers;
using Administration.Model;
using Administration.Model.Common;
using Administration.Model.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Administration.Administration.ControllerAPIs
{
    //[Authorize]
    [RoutePrefix("api/Parameter")]
    public class ParameterController : ApiController
    {
        private readonly IParameterManager _manager;
        private string WebUrl;
        private readonly string appDomain = HttpRuntime.AppDomainAppPath;

        public ParameterController()
        {
            _manager = new ParameterManager();
            WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search([FromUri] PARAMETER_Params model)
        {
            var result = _manager.Search(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add(SYS_PARAMETERS model)
        {
            //model.CREATED_BY = IdentityHelper.UserName;
            model.CREATED_BY = "admin";
            var result = _manager.Add(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update(SYS_PARAMETERS model)
        {
            //model.MODIFIED_BY = IdentityHelper.UserName;
            model.MODIFIED_BY = "admin";
            var result = _manager.Update(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(SYS_PARAMETERS model)
        {
            var result = _manager.Delete(model);
            return Ok(result);
        }
    }
}