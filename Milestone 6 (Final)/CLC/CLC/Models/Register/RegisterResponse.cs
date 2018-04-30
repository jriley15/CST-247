using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/**
 * RegisterResponse
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Contains RegisterResponse model data fields
 * 
 */

namespace ICA3.Models.Register
{
    public class RegisterResponse
    {


        private Boolean success;
        private String message;

        public RegisterResponse()
        {
        }

        public RegisterResponse(bool passed, string message)
        {
            this.success = passed;
            this.message = message;
        }

        public bool Success { get => success; set => success = value; }
        public string Message { get => message; set => message = value; }
    }
}