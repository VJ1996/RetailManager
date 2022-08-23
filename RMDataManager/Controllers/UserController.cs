using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId(); 
            //not passing in string Id. Do it this way to prevent people from guessing
            //for peoples user ids and stealing info

            UserData data = new UserData();

            return data.GetUserById(userId).First();
        }
    }
}
