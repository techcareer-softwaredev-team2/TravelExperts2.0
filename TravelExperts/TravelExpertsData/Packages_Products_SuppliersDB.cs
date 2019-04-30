using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class Packages_Products_SuppliersDB
    {
        // get all PackageIds  
        public static List<int> GetPackageIds()
        {
            List<int> packageIds = new List<int>(); // empty list of package Ids
            int id; // for reading
            //create the connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT DISTINCT PackageId FROM Packages_Products_Suppliers " +
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

        // retrieve ProductSupplierId list with given package ID
        public static List<int> GetProductSupplierIds(int packageId)
        {
            List<int> productSupplierIds = new List<int>(); // empty list of productSupplierIds
            int id; // for reading

            //create the conneciton
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT ProductSupplierId " +
                               "FROM Packages_Products_Suppliers " +
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
                    while (reader.Read()) // if there is a product with this ID
                    {
                        id = (int) reader["ProductSupplierId"];
                        productSupplierIds.Add(id);
                    }
                } // command object recycled
            } // connection object recyled
            return productSupplierIds;
        }

    }
}
