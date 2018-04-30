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
 * Handles register database query logic
 * 
 */

namespace Activity3.Services.Data.Register
{
    public class SecurityDAO
    {

        public bool userExists(RegisterRequest registerRequest)
        {
             bool result = false;

             try
             {
                 // Setup SELECT query with parameters
                 string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username";

                 // Create connection and command
                 using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                 using (SqlCommand cmd = new SqlCommand(query, cn))
                 {
                     // Set query parameters and their values
                     cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = registerRequest.Username;

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

        public bool emailExists(RegisterRequest registerRequest)
        {
            bool result = false;

            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.users WHERE EMAIL=@Email";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = registerRequest.Email;

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

        public bool createUser(RegisterRequest registerRequest)
        {
            bool result = false;

            try
            {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.Users (USERNAME, PASSWORD, EMAIL, FIRSTNAME, LASTNAME, SEX, AGE, STATE) " +
                    "VALUES (@Username, @Password, @Email, @FirstName, @LastName, @Sex, @Age, @State)";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20).Value = registerRequest.Username;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 20).Value = registerRequest.Password;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = registerRequest.Email;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 20).Value = registerRequest.FirstName ?? "N/A";
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 20).Value = registerRequest.LastName ?? "N/A";
                    cmd.Parameters.Add("@Sex", SqlDbType.VarChar, 20).Value = registerRequest.Sex ?? "N/A";
                    cmd.Parameters.Add("@Age", SqlDbType.Int, 11).Value = registerRequest.Age ?? 0;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 20).Value = registerRequest.State ?? "N/A";

                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 1)
                        result = true;
                    else
                        result = false;
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }

            // Return result of create
            return result;
        }
    }
}