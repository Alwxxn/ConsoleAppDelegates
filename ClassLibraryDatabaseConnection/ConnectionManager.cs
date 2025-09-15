using Microsoft.Data.SqlClient;
using System;

namespace ClassLibraryDataBaseConnection
{
    public class ConnectionManager
    {

        public static SqlConnection? openConnection(string _connstring)
        {
            try
            {
                // 1. Check if the connection string is valid before using it.
                if (string.IsNullOrEmpty(_connstring))
                {
                    Console.WriteLine("Error: Connection string is null or empty.");
                    return null;
                }

                // 2. Create a new instance of the connection.
                SqlConnection conn = new SqlConnection(_connstring);

                // 3. Open the connection.
                conn.Open();

                // 4. Return the successfully opened connection.
                return conn;
            }
            catch (SqlException sex) // Catches errors specific to SQL Server
            {
                Console.WriteLine("Oops, a SQL Server error occurred:");
                Console.WriteLine(sex.Message);
                return null; // Return null to indicate failure
            }
            catch (Exception ex) // Catches other general errors
            {
                Console.WriteLine("Oops, an unexpected error occurred:");
                Console.WriteLine(ex.Message);
                return null; // Return null to indicate failure
            }
        }
    }
}