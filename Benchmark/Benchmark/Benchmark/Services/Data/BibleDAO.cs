using Benchmark.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


/*
 * Jordan Riley
 * 4-22-2018
 * BibleDAO class
 * Contains CRUD functions for manipulating
 * bible data through SQL queries
 * 
 */



namespace Benchmark.Services.Data
{
    public class BibleDAO
    {


        public static String CONNECTION_STRING = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=bible;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public Boolean createVerse(BibleVerse bibleVerse)
        {

            try
            {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.verses (TESTAMENT, BOOK, CHAPTER, VERSENUMBER, VERSETEXT) " +
                    "VALUES (@Testament, @Book, @Chapter, @Versenumber, @Versetext)";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Testament", SqlDbType.VarChar, 50).Value = bibleVerse.TestamentSelection;
                    cmd.Parameters.Add("@Book", SqlDbType.VarChar, 50).Value = bibleVerse.BookSelection;
                    cmd.Parameters.Add("@Chapter", SqlDbType.Int, 11).Value = bibleVerse.ChapterNumber;
                    cmd.Parameters.Add("@Versenumber", SqlDbType.Int, 11).Value = bibleVerse.VerseNumber;
                    cmd.Parameters.Add("@Versetext", SqlDbType.VarChar, 1000).Value = bibleVerse.VerseText;

                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    cn.Close();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }
            return false;
        }


        public BibleVerse findVerse(SearchRequest searchRequest)
        {

            try
            {
                // Setup INSERT query with parameters
                string query = "SELECT * FROM dbo.verses WHERE TESTAMENT=@Testament AND BOOK=@Book AND CHAPTER=@Chapter AND VERSENUMBER=@Versenumber";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Testament", SqlDbType.VarChar, 50).Value = searchRequest.TestamentSelection;
                    cmd.Parameters.Add("@Book", SqlDbType.VarChar, 50).Value = searchRequest.BookSelection;
                    cmd.Parameters.Add("@Chapter", SqlDbType.Int, 11).Value = searchRequest.ChapterNumber;
                    cmd.Parameters.Add("@Versenumber", SqlDbType.Int, 11).Value = searchRequest.VerseNumber;

                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        return new BibleVerse(reader["TESTAMENT"].ToString(), reader["BOOK"].ToString(), int.Parse(reader["CHAPTER"].ToString()), int.Parse(reader["VERSENUMBER"].ToString()), reader["VERSETEXT"].ToString());

                    }
                    cn.Close();


                }
            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }



            return null;
        }



    }
}