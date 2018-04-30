﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ICA3.Models;
using System.Data.SqlClient;
using System.Data;
using ICA3.Models.Login;
using ICA3.Models.Register;
using ICA3.Constants;

/**
 * SecurityDAO
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Handles login database query logic
 * 
 */

namespace Activity3.Services.Data.Login
{
    public class SecurityDAO
    {

        
        public Boolean validUser(LoginRequest loginRequest)
        {
             bool result = false;

             try
             {
                 // Setup SELECT query with parameters
                 string query = "SELECT * FROM dbo.users WHERE USERNAME=@Username AND PASSWORD=@Password";

                 // Create connection and command
                 using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                 using (SqlCommand cmd = new SqlCommand(query, cn))
                 {
                     // Set query parameters and their values
                     cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = loginRequest.Username;
                     cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = loginRequest.Password;

                     // Open the connection
                     cn.Open();

                     // Using a DataReader see if query returns any rows
                     SqlDataReader reader = cmd.ExecuteReader();
                     if (reader.HasRows)
                         result = true;
                     else
                         result = false;

                     // Close the connection
                     cn.Close();
                 }

             }
             catch (SqlException e)
             {
                 // TODO: should log exception and then throw a custom exception
                 throw e;
             }

             // Return result of finder
             return result;
        }

       
    }
}