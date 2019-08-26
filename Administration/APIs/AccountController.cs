
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
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserManager _manager;
        private readonly string WebUrl;
        private readonly string appDomain = HttpRuntime.AppDomainAppPath;
        private readonly string folderUpload = string.Format(@"{0}\Content\img\avatar", HttpRuntime.AppDomainAppPath);

        public AccountController()
        {
            _manager = new UserManager();
            WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(Login model)
        {
            ////model.CREATED_BY = IdentityHelper.UserName;
            //model.CREATED_BY = "admin";
            //var result = _manager.Add(model);
            //return Ok(result);
        }
    }
}