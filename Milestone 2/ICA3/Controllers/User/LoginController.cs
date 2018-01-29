using ICA3.Models;
using ICA3.Models.Login;
using ICA3.Services.Business;
using ICA3.Services.Business.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICA3.Controllers.User
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View("Login");
        }


        [HttpPost]
        public ActionResult doLogin(LoginRequest loginRequest)
        {

            LoginResponse response;

            if (ModelState.IsValid) {
            
                SecurityService ss = new SecurityService();
                response = ss.Authenticate(loginRequest);

                if (response.Success)
                {
                    return View("LoginPassed", loginRequest);

                }
            }
            else
            {
                string errors = string.Join("<br/> ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                response = new LoginResponse(false, errors);
            }
            return View("LoginFailed", response);


        }

    }
}