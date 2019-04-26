using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class ProductsDB
    {
        // get all Products IDs
        public static List<int> GetProductsIds()
        {
            List<int> productIds = new List<int>(); // empty list of product Ids
            int id; // for reading
            //create the connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT ProductId FROM Products "+
                               "ORDER BY ProductId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    while (reader.Read())
                    {
                        id = (int)reader["ProductId"];
                        productIds.Add(id);
                    }
                } // command object recycled
            } // connection object recyled
            return productIds;
        }

        // retrieve product info with given product ID
        public static Products GetProductById(int productId)
        {
            Products product = null;

            //create the conneciton
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT ProductId, ProdName " + 
                               "FROM Products " +
                               "WHERE ProductId=@ProductId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    //supply parameter value
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    //open the connection
                    connection.Open();
                    //run the command
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    //build product object to return
                    if (reader.Read()) // if there is a product with this ID
                    {
                        product = new Products();
                        product.ProductId = (int)reader["ProductId"];
                        product.ProdName = reader["ProdName"].ToString();
                    }
                } // command object recycled
            } // connection object recyled
            return product;
        }

        // insert a new product into Products table
        // return new product ID
        public static int AddProduct(Products product)
        {
            int productId = 0;
            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create INSERT command
                // CustomerID is IDENTITY so no value provided
                string insertStatement = "INSERT INTO Products(ProdName) " +
                                         "OUTPUT inserted.ProductId " +
                                         "VALUES(@ProdName)";

                using (SqlCommand cmd = new SqlCommand(insertStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    cmd.Parameters.AddWithValue("@ProdName", product.ProdName);
                    // execute INSERT command
                    try
                    {
                        // open the connection
                        connection.Open();
                        // execute insert command and get inserted ID
                        productId = (int)cmd.ExecuteScalar();
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
            return productId;
        }

        // delete a product from Products table
        // return indicator of success
        public static bool DeleteProduct(Products product)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string deleteStatement =
                    "DELETE FROM Products " +
                    "WHERE ProductId=@ProductId " + // need for identification
                    "AND ProdName=@ProdName";  // the AND is to ensure no one is updating this product
                using (SqlCommand cmd = new SqlCommand(deleteStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@ProdName", product.ProdName);

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
        public static bool UpdateProduct(Products oldProduct, Products newProduct)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string updateStatement =
                    "UPDATE Products SET " +
                    "ProdName=@NewProdName " + // only product name can be updated
                    "WHERE ProductId=@OldProductId " + // need for identification
                    "AND ProdName=@OldProdName";  // the AND is to ensure no one is updating this product
                using (SqlCommand cmd = new SqlCommand(updateStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    cmd.Parameters.AddWithValue("@NewProdName", newProduct.ProdName);
                    cmd.Parameters.AddWithValue("@OldProductId", oldProduct.ProductId);
                    cmd.Parameters.AddWithValue("@OldProdName", oldProduct.ProdName);

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
    }// class
}// namespace
