using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class PackagesDB
    {
        // get all Package IDs
        public static List<int> GetPackageIds()
        {
            List<int> packageIds = new List<int>(); // empty list of package Ids
            int id; // for reading
            //create the connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT PackageId FROM Packages " +
                               "ORDER BY PackageId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    while (reader.Read())
                    {
                        id = (int)reader["PackageId"];
                        packageIds.Add(id);
                    }
                } // command object recycled
            } // connection object recyled
            return packageIds;
        }

        // retrieve package info with given package ID
        public static Packages GetPackageById(int packageId)
        {
            Packages package = null;

            //create the conneciton
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT PackageId, PkgName,PkgStartDate, PkgEndDate, " +
                               "PkgDesc, PkgBasePrice, PkgAgencyCommission "+
                               "FROM Packages" +
                               "WHERE PackageId=@PackageId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    //supply parameter value
                    cmd.Parameters.AddWithValue("@PackageId", packageId);
                    //open the connection
                    connection.Open();
                    //run the command
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    //build product object to return
                    if (reader.Read()) // if there is a product with this ID
                    {
                        package = new Packages();
                        package.PackageId = (int)reader["PackageId"];
                        package.PkgName = reader["PkgName"].ToString();

                        int col_st = reader.GetOrdinal("PkgStartDate"); //column number of Start Date
                        if (reader.IsDBNull(col_st)) // if reader contains DBNull in this column
                            package.PkgStartDate = null; // make it null in the object
                        else // it is not null
                            package.PkgStartDate = Convert.ToDateTime(reader["PkgStartDate"]);

                        int col_en = reader.GetOrdinal("PkgEndDate"); //column number of End Date
                        if (reader.IsDBNull(col_en)) // if reader contains DBNull in this column
                            package.PkgEndDate = null; // make it null in the object
                        else // it is not null
                            package.PkgEndDate = Convert.ToDateTime(reader["PkgStartDate"]);

                        package.PkgDesc = reader["PkgDesc"].ToString();
                        package.PkgBasePrice = (decimal)reader["PkgBasePrice"];
                        package.PkgAgencyCommission = (decimal)reader["PkgAgencyCommission"];

                    }
                } // command object recycled
            } // connection object recyled
            return package;
        }
    }
}
