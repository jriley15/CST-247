﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICA3.Models.Login
{
    public class LoginResponse
    {


        private Boolean success;
        private String message;

        public LoginResponse()
        {
        }

        public LoginResponse(bool passed, string message)
        {
            this.success = passed;
            this.message = message;
        }

        public bool Success { get => success; set => success = value; }
        public string Message { get => message; set => message = value; }
    }
}