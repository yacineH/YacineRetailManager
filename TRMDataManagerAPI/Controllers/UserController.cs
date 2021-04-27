using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TRMDLL.DataAccess;
using TRMDLL.Models;


namespace TRMDataManagerAPI.Controllers
{
    [Authorize]
    [RoutePrefix("/api/User")]
    public class UserController : ApiController
    {
        public List<UserModel> GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();
            
            return data.GetUserById(userId);
        }
    }
}
