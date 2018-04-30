using CLC.Services.Business;
using CLC.Services.Utility;
using ICA3.Models;
using ICA3.Models.Login;
using ICA3.Services.Business;
using ICA3.Services.Business.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/**
 * LoginController
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Handles flow of login forms and logic
 * 
 */

namespace ICA3.Controllers.User
{
    public class LoginController : Controller
    {


        private readonly ILogger logger;


        public LoginController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            logger.Info("LoginControler::Index()");
            return View("Login");
        }


        [HttpPost]
        public ActionResult doLogin(LoginRequest loginRequest)
        {
            logger.Info("LoginControler::doLogin()");
            LoginResponse response;

            if (ModelState.IsValid) {
                logger.Info("LoginControler::doLogin() Model is valid");
                SecurityService ss = new SecurityService();
                response = ss.Authenticate(loginRequest);

                if (response.Success)
                {
                    logger.Info("LoginControler::doLogin() Successful response");
                    //load user model and throw in session var
                    UserService userService = new UserService();
                    var user = userService.loadUser(loginRequest);
                    this.Session["user"] = user;


                    return View("LoginPassed", loginRequest);

                }
            }
            else
            {
                logger.Info("LoginControler::doLogin() Model invalid");
                string errors = string.Join("<br/> ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                response = new LoginResponse(false, errors);
            }

            logger.Info("LoginControler::doLogin() Login failed");
            return View("LoginFailed", response);


        }

    }
}