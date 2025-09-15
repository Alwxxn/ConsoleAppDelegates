
using System;
using ClassLibraryDataBaseConnection;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ConsoleAppADODemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //first usecase - direct connection
            Console.WriteLine("Trying to connect to Sql Server Database....");

            //step 1: Get Connectionstring from APP.config
            string conString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

            /*  //step 2: establish connection -SqlConnection
              using (SqlConnection connection = new SqlConnection(conString))
              {
                  try
                  {
                      //step 3 : open connection
                      connection.Open();
                      Console.WriteLine("Connection established successfully");
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine("Something went wrong.\n" + ex);
                  }
                  finally
                  {
                      //step 4 : ensure the connection is closed
                      if (connection.State == ConnectionState.Open)
                      {
                          connection.Close();
                      }

                  }*/
            //second use case-use dll

            //step 2: establish connection -SqlConnection
            using (SqlConnection connection = ConnectionManager.openConnection(conString))
            {
                try
                {
                    if (connection != null)
                    {
                        Console.WriteLine("Connection established successfully");

                        #region Traditional Script
                        /*
                        // First, let's see what tables exist in the database
                        string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                        
                        //creating sqlcommand object
                        SqlCommand command = new SqlCommand(query, connection);
                        //executing the sql query
                        SqlDataReader reader = command.ExecuteReader();
                        //looping through each record
                        Console.WriteLine("Available tables in the database:");
                        while (reader.Read())
                        {
                            Console.WriteLine("Table: " + reader["TABLE_NAME"]);
                        }
                        reader.Close();
                        
                        
                        
                        while (selectReader.Read())
                        {
                            Console.WriteLine($"ID: {selectReader["Id"]}, Name: {selectReader["Name"]}, Description: {selectReader["Description"]}");
                        }
                        selectReader.Close();
                        #endregion

                         #region Call Stored Procedure
                        SqlCommand command = new SqlCommand("sp_GetAllEmployees", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader =  command.ExecuteReader();

                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Id"] + " " + reader["Name"] + " " +
                                reader["DateOfJoin"] + " " + reader["Salary"]);
                        }*/
                        #endregion

                        #region
                        
                        ////creating sqlcommand object
                        //SqlCommand command = new SqlCommand("sp_GetAllEmployees", connection);
                        //command.CommandType = CommandType.StoredProcedure;
                        ////executing the sql query
                        //SqlDataReader reader = command.ExecuteReader();

                        ////Column Heading
                        //Console.WriteLine("----------------------------------------------------------");
                        //Console.WriteLine($"{"ID",-5} {"Name",20} {"DateOfJoin",-15} {"Salary",-10}");
                        //Console.WriteLine("----------------------------------------------------------");


                        ////looping through each record
                        //while (reader.Read())
                        //{
                        //    /*Console.WriteLine(reader["Id"] + " " + reader["Name"] + " " +
                        //        reader["DateOfJoin"] + " " + reader["Salary"]);*/

                        //    Console.WriteLine($"{reader["Id"],-5} {reader["Name"],20} {Convert.ToDateTime(reader["DateOfJoin"]).ToString("dd-MM-yyyy"),-15} {reader["Salary"],-10}");
                        //    //we can use index of the data reader object
                        //    //Console.WriteLine(reader[0] + " " + reader[1] + " " +
                        //    //    reader[2] + " " + reader[3]);
                        //}
                        
                       
                        #endregion

                    #region
                        Console.WriteLine("Enter Employee Register Number");
                        int SearchId = Convert.ToInt32(Console.ReadLine());

                        //Call Stored Procedure with Parameter
                        SqlCommand cmd = new SqlCommand("sp_GetEmployeeId", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", SearchId);
                        SqlDataReader rdr = cmd.ExecuteReader();

                        //Column Heading
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine($"{"ID",-5} {"Name",20} {"DateOfJoin",-15} {"Salary",-10}");
                        Console.WriteLine("----------------------------------------------------------");


                        //looping through each record
                        while (rdr.Read())
                        {
                            /*Console.WriteLine(reader["Id"] + " " + reader["Name"] + " " +
                                reader["DateOfJoin"] + " " + reader["Salary"]);*/

                            Console.WriteLine($"{rdr["Id"],-5} {rdr["Name"],20} {Convert.ToDateTime(rdr["DateOfJoin"]).ToString("dd-MM-yyyy"),-15} {rdr["Salary"],-10}");
                        }
                     #endregion



                        }
                    else
                    {
                        Console.WriteLine("Failed to establish connection,Check the console for errors");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong.\n" + ex);
                }
                finally
                {
                    //step 4 : ensure the connection is closed
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                }
            }
            Console.ReadKey();

        }
    }
}