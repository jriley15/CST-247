using Activity3.Services.Data;
using Activity3.Services.Data.Register;
using ICA3.Models;
using ICA3.Models.Login;
using ICA3.Models.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ICA3.Services.Business.Register
{
    public class SecurityService
    {



        public RegisterResponse Authenticate(RegisterRequest registerRequest)
        {

            RegisterResponse response = new RegisterResponse();
            response.Success = false;

            SecurityDAO dataService = new SecurityDAO();



            if (dataService.userExists(registerRequest))
            {
                response.Message = "Username already exists.";
            }
            else if (dataService.emailExists(registerRequest))
            {
                response.Message = "Email already in use.";
            }
            else if (dataService.createUser(registerRequest))
            {
                response.Success = true;
            }


            return response;

        }

    }
}