using CLC.Models.Game;
using CLC.Models.User;
using ICA3.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/**
 * GameDAO
 * Authors: Jordan Riley & Antonio Jabrail
 * Date: 4-29-2018
 * Handles game database query logic
 * 
 */

namespace CLC.Services.Data.Game
{
    public class GameDAO
    {



        public Grid findGrid(User user)
        {
            Grid g = null;
            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.grids WHERE USERID=@id";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = user.Id;


                    // Open the connection
                    cn.Open();

                    // Using a DataReader see if query returns any rows
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = int.Parse(reader["ID"].ToString());
                        int rows = int.Parse(reader["ROWS"].ToString());
                        int cols = int.Parse(reader["COLS"].ToString());
                        int USER_ID = int.Parse(reader["USERID"].ToString());
                        Boolean GAMEOVER = Boolean.Parse(reader["GAMEOVER"].ToString());
                        int clicks = int.Parse(reader["CLICKS"].ToString());

                        g = new Grid(ID, rows, cols, USER_ID, GAMEOVER, clicks);
                        g.Cells = new Cell[cols, rows];
                    }

                    // Close the connection
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }


            if (g != null)
            {
                try
                {
                    // Setup SELECT query with parameters
                    string query = "SELECT * FROM dbo.cells WHERE GRIDID=@id";

                    // Create connection and command
                    using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        // Set query parameters and their values
                        cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = g.Id;


                        // Open the connection
                        cn.Open();

                        // Using a DataReader see if query returns any rows
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int ID = int.Parse(reader["ID"].ToString());
                            int x = int.Parse(reader["X"].ToString());
                            int y = int.Parse(reader["Y"].ToString());
                            Boolean bomb = Boolean.Parse(reader["BOMB"].ToString());
                            Boolean visited = Boolean.Parse(reader["VISITED"].ToString());
                            int live = int.Parse(reader["LIVENEIGHBORS"].ToString());
                            int gridId = int.Parse(reader["GRIDID"].ToString());

                            Cell c = new Cell(x, y);
                            c.Id = ID;
                            c.Bomb = bomb;
                            c.Visited = visited;
                            c.LiveNeighbors = live;
                            g.Cells[x, y] = c;

                        }

                        // Close the connection
                        cn.Close();
                    }

                }
                catch (SqlException e)
                {
                    // TODO: should log exception and then throw a custom exception
                    throw e;
                }
            }

            return g;


        }


        public void createGrid(Grid grid)
        {

            int gridID = -1;
            try
            {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.grids (ROWS, COLS, USERID, GAMEOVER, CLICKS) " +
                    "VALUES (@Rows, @Cols, @User_ID, @GameOver, @clicks) SELECT SCOPE_IDENTITY()";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                    cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int, 11).Value = grid.Userid;
                    cmd.Parameters.Add("@GameOver", SqlDbType.Bit).Value = grid.GameOver;
                    cmd.Parameters.Add("@clicks", SqlDbType.Int, 11).Value = grid.Clicks;


                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();
                    gridID = Convert.ToInt32(cmd.ExecuteScalar());

                    cn.Close();



                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }


            try
            {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.cells (X, Y, BOMB, VISITED, LIVENEIGHBORS, GRIDID) " +
                    "VALUES (@x, @y, @bomb, @visited, @live, @grid)";

                // Create connection and command
                for (int y = 0; y < grid.Rows; y++)
                {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            // Set query parameters and their values
                            cmd.Parameters.Add("@x", SqlDbType.Int, 11).Value = grid.Cells[x, y].X;
                            cmd.Parameters.Add("@y", SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            cmd.Parameters.Add("@bomb", SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            cmd.Parameters.Add("@visited", SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            cmd.Parameters.Add("@live", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            cmd.Parameters.Add("@grid", SqlDbType.Int, 11).Value = gridID;

                            // Open the connection, execute INSERT, and close the connection
                            cn.Open();
                            int rows = cmd.ExecuteNonQuery();
                            cn.Close();



                        }
                    }
                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }



        }



        public void updateGrid(Grid grid)
        {

            try
            {
                // Setup INSERT query with parameters

                string query = "UPDATE dbo.grids SET ROWS = @Rows, COLS = @Cols, USERID = @User_ID, GAMEOVER = @GameOver, CLICKS = @clicks WHERE ID=@id";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                    cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int, 11).Value = grid.Userid;
                    cmd.Parameters.Add("@GameOver", SqlDbType.Bit).Value = grid.GameOver;
                    cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = grid.Id;
                    cmd.Parameters.Add("@clicks", SqlDbType.Int, 11).Value = grid.Clicks;

                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    cn.Close();



                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }


            try
            {
                // Setup INSERT query with parameters

                string query = "UPDATE dbo.cells SET X = @x, Y = @y, BOMB = @bomb, VISITED = @visited, LIVENEIGHBORS = @live, " +
                    "GRIDID = @grid WHERE ID=@id";



                // Create connection and command
                for (int y = 0; y < grid.Rows; y++)
                {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            // Set query parameters and their values
                            cmd.Parameters.Add("@x", SqlDbType.Int, 11).Value = grid.Cells[x, y].X;
                            cmd.Parameters.Add("@y", SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            cmd.Parameters.Add("@bomb", SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            cmd.Parameters.Add("@visited", SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            cmd.Parameters.Add("@live", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            cmd.Parameters.Add("@grid", SqlDbType.Int, 11).Value = grid.Id;
                            cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = grid.Cells[x, y].Id;
                            // Open the connection, execute INSERT, and close the connection
                            cn.Open();
                            int rows = cmd.ExecuteNonQuery();
                            cn.Close();

                        }
                    }
                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }



        }




        public void deleteGrid(User user)
        {

            try
            {
                string query = "DELETE FROM dbo.grids WHERE USERID=@Id ";


                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }


        }

        //checks if game results have already been published 
        public Boolean gridPublished(Grid g)
        {
            bool result = false;

            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.games WHERE ID=@id";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = g.Id;

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


        //saves game stats to db table 'games'
        public void publishGrid(Grid g)
        {
            try
            {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.games (GRIDID, USERID, CLICKS) " +
                    "VALUES (@Gridid, @Userid, @Clicks)";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Gridid", SqlDbType.Int, 11).Value = g.Id;
                    cmd.Parameters.Add("@Userid", SqlDbType.Int, 11).Value = g.Userid;
                    cmd.Parameters.Add("@Clicks", SqlDbType.Int, 11).Value = g.Clicks;

                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();

                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }
        }


        //returns all game stats in a list
        public List<PublishedGame> getAllStats()
        {
            List<PublishedGame> games = new List<PublishedGame>();

            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.games";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {

                    // Open the connection
                    cn.Open();

                    // Using a DataReader see if query returns any rows
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = int.Parse(reader["ID"].ToString());
                        int gridid = int.Parse(reader["GRIDID"].ToString());
                        int userid = int.Parse(reader["USERID"].ToString());
                        int clicks = int.Parse(reader["CLICKS"].ToString());

                        games.Add(new PublishedGame(ID, gridid, userid, clicks));
                    }

                    // Close the connection
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }

            return games;
        }


        //test db connectivity
        public Boolean testService()
        {
            List<PublishedGame> games = new List<PublishedGame>();

            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT 1";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(DB.CONNECTION_STRING))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {

                    // Open the connection
                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return true;
                    }
                    // Close the connection
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                return false;
            }

            return false;
        }




    }


}