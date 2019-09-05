
using Administration.BAL.IManagers;
using Administration.BAL.Managers;
using Administration.Model;
using Administration.Model.Common;
using Administration.Model.Utilities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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

        //[HttpPost]
        //[Route("Login")]
        //public IHttpActionResult Login(Login model)
        //{
        //    ////model.CREATED_BY = IdentityHelper.UserName;
        //    //model.CREATED_BY = "admin";
        //    var result = _manager.Add(model);
        //    return Ok(result);
        //}

        [HttpPost]
        public IHttpActionResult Login([FromBody] Login login)
        {
            var result = new Result();
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            bool isUsernamePasswordValid = false;

            if (login != null)
                isUsernamePasswordValid = login.USER_NAME.ToLower()=="admin" && login.PASSWORD == "admin" ? true : false;
            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                string token = createToken(login.USER_NAME);

                ////return the token
                //return Ok<string>(token);
                result.Code = (short)HttpStatusCode.OK;
                result.Data = token;
                result.Message = "Login successful!";
            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                //loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                //response = ResponseMessage(loginResponse.responseMsg);
                //return response;
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                //result.Data = token;
                result.Message = "Login unsuccessful!";
            }

            return Ok(result);
        }

        private string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:11111", audience: "http://localhost:11111",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}