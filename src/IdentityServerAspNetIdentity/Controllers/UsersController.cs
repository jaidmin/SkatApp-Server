using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace IdentityServerAspNetIdentity.Controllers
{
    [Produces("application/json")]
    
    //[Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
       
        public IEnumerable<String> all()
        {
            var list2 = from user in _userManager.Users select user.UserName;
            return list2;
        }
        [HttpGet]
        public String current()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            var is_authenticated = _signInManager.IsSignedIn(currentUser);
            if (is_authenticated == true)
            {
                return "you are authenticated";
            }
            else
            {
                return "you are not authenticated/logged in";
            };
        }
        /*public IEnumerable<string> signedin() {
            var users = _userManager.Users;
            var logged_in = from user in users where  _signInManager.IsSignedIn((ClaimsIdentity)user) == true select user;
            return logged_in;

}*/

        
    }
}