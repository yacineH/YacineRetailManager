﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TRMDataManagerAPI.Models;
using TRMDLL.DataAccess;
using TRMDLL.Models;


namespace TRMDataManagerAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();
            
            return data.GetUserById(userId).First();
        }

        //get users in role with EF (database + Identity)
        //gestion de roles with groups
        [HttpGet]
        [Route("api/User/Admin/GetAllUsers")]
        //[AllowAnonymous]
        [Authorize(Roles ="Admin")]
        public List<ApplicationUserModel> GetAllUsers()
        {

            List<ApplicationUserModel> output = new List<ApplicationUserModel>();

            using (var context=new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();

                foreach (var user in users)
                {
                    var u = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Email = user.Email
                    };
                    foreach (var r in user.Roles )
                    {
                        u.Roles.Add(r.RoleId,roles.Where(x=>x.Id==r.RoleId).First().Name);
                    }
                    output.Add(u);
                }

            }
            return output;
        }
    }
}
