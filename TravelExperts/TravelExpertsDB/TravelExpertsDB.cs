using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{

    public static class TravelExpertsDB
    {
        // database connection
        public static SqlConnection GetConnection()
        {
            //connection string
            string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=TravelExperts;Integrated Security=True";
            return new SqlConnection(connectionString);
        }

        // get table list
        public static List<string> GetTableNames()
        {
            List<string> tableNames = new List<string>(); // empty list of table names
            string name; // for reading
            //create the connection
            using (SqlConnection connection = GetConnection())
            {
                // create select command
                string query = "SELECT TABLE_NAME FROM  information_schema.tables " +
                               "WHERE TABLE_NAME = 'Products' " +
                               "OR TABLE_NAME = 'Suppliers' " +
                               "OR TABLE_NAME = 'Products_Suppliers' " +
                               "OR TABLE_NAME = 'Packages_Products_Suppliers' " +
                               "OR TABLE_NAME = 'Packages'";

                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    while (reader.Read())
                    {
                        name = reader["TABLE_NAME"].ToString();
                        tableNames.Add(name);
                    }
                } // command object recycled
            } // connection object recyled
            return tableNames;
        }
    }
}
