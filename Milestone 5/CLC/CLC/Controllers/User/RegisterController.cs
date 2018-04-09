using ICA3.Models;
using ICA3.Models.Register;
using ICA3.Services.Business;
using ICA3.Services.Business.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICA3.Controllers.User
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View("Register");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult doRegister(RegisterRequest registerRequest)
        {
            SecurityService ss = new SecurityService();
            RegisterResponse response;

            if (ModelState.IsValid)
            {
                response = ss.Authenticate(registerRequest);

                if (response.Success)
                {
                    return View("RegisterPassed", registerRequest);

                }
            } else
            {
                string errors = string.Join("<br/> ", ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));
                response = new RegisterResponse(false, errors);
            }

            return View("RegisterFailed", response);

        }

    }
}