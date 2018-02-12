using Activity3.Services.Data;
using Activity3.Services.Data.Login;
using CLC.Models.User;
using ICA3.Models;
using ICA3.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ICA3.Services.Business.Login
{
    public class SecurityService
    {



        public LoginResponse Authenticate(LoginRequest loginRequest)
        {
            
            LoginResponse response = new LoginResponse();
            response.Success = false;

            SecurityDAO dataService = new SecurityDAO();

            if (dataService.validUser(loginRequest))
            {
                response.Success = true;
            }
            else
            {
                response.Message = "Invalid username or password.";
            }

            return response;

        }

    }
}