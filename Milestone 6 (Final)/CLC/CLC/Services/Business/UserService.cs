using CLC.Models.User;
using CLC.Services.Data;
using ICA3.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/**
 * UserService
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Handles user security functionality and logic
 * 
 */

namespace CLC.Services.Business
{
    public class UserService
    {


        public Boolean loggedIn(Controller c)
        {
            return c.Session["user"] != null;
        }

        public User loadUser(LoginRequest loginRequest)
        {
            UserDAO userDAO = new UserDAO();

            return userDAO.findUser(loginRequest);
        }



    }
}