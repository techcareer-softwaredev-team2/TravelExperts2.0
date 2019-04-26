using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class Products_SuppliersDB
    {
        // get all ProductSupplier Ids
        public static List<int> GetProductSupplierIds()
        {
            List<int> productSupplierIds = new List<int>(); // empty list of ProductSupplier Ids
            int id; // for reading
            //create the connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT ProductSupplierId FROM Products " +
                               "ORDER BY ProductSupplierId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    while (reader.Read())
                    {
                        id = (int)reader["ProductSupplierIds"];
                        productSupplierIds.Add(id);
                    }
                } // command object recycled
            } // connection object recyled
            return productSupplierIds;
        }

        // retrieve productSupplier info with given productSupplier Id
        public static Products_Suppliers GetProductSupplierById(int productSupplierId)
        {
            Products_Suppliers productSupplier = null;

            //create the conneciton
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT ProductSupplierId, ProductId, SupplierId " +
                               "FROM Products_Suppliers " +
                               "WHERE ProductSupplierId=@ProductSupplierId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    //supply parameter value
                    cmd.Parameters.AddWithValue("@ProductSupplierId", productSupplierId);
                    //open the connection
                    connection.Open();
                    //run the command
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    //build product object to return
                    if (reader.Read()) // if there is a product with this ID
                    {
                        productSupplier = new Products_Suppliers();
                        productSupplier.ProductSupplierId = (int)reader["ProductSupplierId"];

                        int col_pro = reader.GetOrdinal("ProductId"); //column number of ProductId
                        if (reader.IsDBNull(col_pro)) // if reader contains DBNull in this column
                            productSupplier.ProductId = null; // make it null in the object
                        else // it is not null
                            productSupplier.ProductId = (int)reader["ProductId"];

                        int col_sup = reader.GetOrdinal("SupplierId"); //column number of SupplierId
                        if (reader.IsDBNull(col_sup)) // if reader contains DBNull in this column
                            productSupplier.SupplierId = null; // make it null in the object
                        else // it is not null
                            productSupplier.SupplierId = (int)reader["SupplierId"];
                    }
                } // command object recycled
            } // connection object recyled
            return productSupplier;
        }

        // insert a new product supplier into Products_Suppliers table
        // return new product_supplier Id
        public static int AddProductSupplier(Products_Suppliers productSupplier)
        {
            int productSupplierId = 0;
            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create INSERT command
                // CustomerID is IDENTITY so no value provided
                string insertStatement = "INSERT INTO Products_Supplier(ProductId, SupplierId) " +
                                         "OUTPUT inserted.ProductSupplierId " +
                                         "VALUES(@ProductId, @SupplierId)";

                using (SqlCommand cmd = new SqlCommand(insertStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    if (productSupplier.ProductId == null)
                        cmd.Parameters.AddWithValue("@ProductId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ProductId", productSupplier.ProductId);

                    if (productSupplier.SupplierId == null)
                        cmd.Parameters.AddWithValue("@SupplierId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@SupplierId", productSupplier.SupplierId);
                    // execute INSERT command
                    try
                    {
                        // open the connection
                        connection.Open();
                        // execute insert command and get inserted ID
                        productSupplierId = (int)cmd.ExecuteScalar();
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
            return productSupplierId;
        }

        // delete a product supplier from Products_Suppliers table
        // return indicator of success
        public static bool DeleteProductSupplier(Products_Suppliers productSupplier)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string deleteStatement =
                    "DELETE FROM Products_Suppliers " +
                    "WHERE ProductSupplierId=@ProductSupplierId " + // need for identification
                    "AND (ProductId=@ProductId OR ProductId IS NULL AND @ProductId IS NULL) " +
                    "AND (SupplierId=@SupplierId OR SupplierId IS NULL AND @SupplierId IS NULL)";  // the AND is to ensure no one is updating this product                
                using (SqlCommand cmd = new SqlCommand(deleteStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    if (productSupplier.ProductId == null)
                        cmd.Parameters.AddWithValue("@ProductId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ProductId", productSupplier.ProductId);

                    if (productSupplier.SupplierId == null)
                        cmd.Parameters.AddWithValue("@SupplierId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@SupplierId", productSupplier.SupplierId);

                    try
                    {
                        // open the connection
                        connection.Open();
                        // execute the delete command 
                        int count = cmd.ExecuteNonQuery(); // returns the number of rows affected 

                        // check if successful
                        if (count > 0)
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
            //return the indicator of success
            return success;
        }

        // Update Product: oldProduct - before update, newProduct - new data
        public static bool UpdateProductSupplier(Products_Suppliers oldProductSupplier, Products_Suppliers newProductSupplier)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string updateStatement =
                    "UPDATE Products_Suppliers SET " +
                    "ProductId=@NewProductId " +
                    "SupplierId=@NewSupplierId " +
                    "WHERE ProductSupplierId=@OldProductSupplierId " + // need for identification
                    "AND (ProductId=@OldProductId OR ProductId IS NULL AND @OldProductId IS NULL) " +
                    "AND (SupplierId=@OldSupplierId OR SupplierId IS NULL AND @OldSupplierId IS NULL)";  // the AND is to ensure no one is updating this product
                using (SqlCommand cmd = new SqlCommand(updateStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    if (newProductSupplier.ProductId == null)
                        cmd.Parameters.AddWithValue("@NewProductId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@NewProductId", newProductSupplier.ProductId);

                    if (newProductSupplier.SupplierId == null)
                        cmd.Parameters.AddWithValue("@NewSupplierId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@NewSupplierId", newProductSupplier.SupplierId);

                    if (oldProductSupplier.ProductId == null)
                        cmd.Parameters.AddWithValue("@OldProductId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@OldProductId", oldProductSupplier.ProductId);

                    if (oldProductSupplier.SupplierId == null)
                        cmd.Parameters.AddWithValue("@OldSupplierId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@OldSupplierId", oldProductSupplier.SupplierId);

                    try
                    {
                        // open the connection
                        connection.Open();
                        // execute the delete command 
                        int count = cmd.ExecuteNonQuery(); // returns the number of rows affected 

                        // check if successful
                        if (count > 0)
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
            //return the indicator of success
            return success;
        }
    }
}
