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
                        id = (int)reader["ProductSupplierId"];
                        productSupplierIds.Add(id);
                    }
                } // command object recycled
            } // connection object recyled
            return productSupplierIds;
        }

        // add ProductSupplierId to a package by given package ID
        public static bool AddPackageProductSupplier(int packageId, int productSupplierId)
        {
            bool success=false;
            int rowcount; // count the rows affected
            //create the conneciton
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string insertStatement = "INSERT INTO Packages_Products_Suppliers(PackageId,ProductSupplierId) " +
                                         "OUTPUT @@ROWCOUNT " +
                                         "VALUES(@PackageId, @ProductSupplierId) ";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(insertStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    cmd.Parameters.AddWithValue("@PackageId", packageId);
                    cmd.Parameters.AddWithValue("@ProductSupplierId", productSupplierId);
                    // execute INSERT command
                    try
                    {
                        // open the connection
                        connection.Open();
                        // execute insert command and get inserted ID
                        rowcount= (int)cmd.ExecuteScalar();
                        if (rowcount > 0)
                            success = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally // executes always
                    {
                        connection.Close();
                    }
                }
            }
            return success;
        }

        // delete a package product supplier from package product supplier table
        // return indicator of success
        public static bool DeletePackageProductSupplier(int packageId, int productSupplierId)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string deleteStatement =
                    "DELETE FROM Packages_Products_Suppliers " +
                    "WHERE  PackageId=@PackageId " +
                    "AND ProductSupplierId=@ProductSupplierId";

                using (SqlCommand cmd = new SqlCommand(deleteStatement, connection))
                {
                    //supply parameter value
                    cmd.Parameters.AddWithValue("@PackageId", packageId);
                    cmd.Parameters.AddWithValue("@ProductSupplierId", productSupplierId);

                    // open the connection
                    connection.Open();
                    //execute UPDATE command
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                        success = true;
                }
                //return the indicator of success
                return success;
            }
        }

    }
}


