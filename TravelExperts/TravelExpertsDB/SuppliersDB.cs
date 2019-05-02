using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class SuppliersDB
    {
        // get all Suppliers IDs
        public static List<int> GetSuppliersIds()
        {
            List<int> supplierIds = new List<int>(); // empty list of supplier Ids
            int id; // for reading
            //create the connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT SupplierId FROM Suppliers " +
                               "ORDER BY SupplierId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    while (reader.Read())
                    {
                        id = (int)reader["SupplierId"];
                        supplierIds.Add(id);
                    }
                } // command object recycled
            } // connection object recyled
            return supplierIds;
        }

        // retrieve supplier info with given supplier ID
        public static Suppliers GetSupplierById(int supplierId)
        {
            Suppliers supplier = null;

            //create the conneciton
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT SupplierId, SupName " +
                               "FROM Suppliers " +
                               "WHERE SupplierId=@SupplierId";
                // any exception not handled here is automaticlly thrown to the form
                // where the method was called
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    //supply parameter value
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                    //open the connection
                    connection.Open();
                    //run the command
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    // close connection as soon as done with reading
                    //build product object to return
                    if (reader.Read()) // if there is a product with this ID
                    {
                        supplier = new Suppliers();
                        supplier.SupplierId = (int)reader["SupplierId"];
                        supplier.SupName = reader["SupName"].ToString();
                    }
                } // command object recycled
            } // connection object recyled
            return supplier;
        }

        // insert a new supplier into Suppliers table
        // return new supplier ID
        public static int AddSupplier(Suppliers supplier)
        {
            int supplierId = 0;
            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create INSERT command
                // CustomerID is IDENTITY so no value provided
                string insertStatement = "INSERT INTO Suppliers(SupName) " +
                                         "OUTPUT inserted.SupplierId " +
                                         "VALUES(@SupName)";

                using (SqlCommand cmd = new SqlCommand(insertStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    cmd.Parameters.AddWithValue("@SupName", supplier.SupName);
                    // execute INSERT command
                    try
                    {
                        // open the connection
                        connection.Open();
                        // execute insert command and get inserted ID
                        supplierId = (int)cmd.ExecuteScalar();
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
            return supplierId;
        }

        // delete a supplier from Suppliers table
        // return indicator of success
        public static bool DeleteSupplier(Suppliers supplier)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string deleteStatement1 =
                    "DELETE FROM Packages_Products_Suppliers " +
                    "WHERE  ProductSupplierId IN " +
                    "(SELECT ProductSupplierId " +
                    "FROM Products_Suppliers " +
                    "WHERE SupplierId=@SupplierId)";

                string deleteStatement2 =
                    "DELETE FROM Products_Suppliers " +
                    "WHERE SupplierId=@SupplierId";

                string deleteStatement3 =
                    "DELETE FROM Suppliers " +
                    "WHERE SupplierId=@SupplierId " + // need for identification
                    "AND (SupName=@SupName OR SupName IS NULL AND @SupName IS NULL)";  // the AND is to ensure no one is updating this product

                connection.Open();
                // start a location transaction
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;
                //supply paramter value, this way can avoid sql injection problem
                command.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
                command.Parameters.AddWithValue("@SupName", supplier.SupName);

                try
                {
                    // Execute three separate commands
                    command.CommandText = deleteStatement1;
                    command.ExecuteNonQuery();
                    command.CommandText = deleteStatement2;
                    command.ExecuteNonQuery();
                    command.CommandText = deleteStatement3;
                    command.ExecuteNonQuery();

                    // Commit the transaction
                    sqlTran.Commit();
                    success = true;
                }
                catch (Exception)
                {
                    try
                    {
                        //Atttemp to roll back the transaction
                        sqlTran.Rollback();
                        success = false;
                    }
                    catch (Exception exRollback)
                    {
                        throw exRollback;
                    }
                }
                finally // executes always
                {
                    connection.Close();
                }
            }
            //return the indicator of success
            return success;
        }

        // Update Supplier: oldSupplier - before update, newSupplier - new data
        public static bool UpdateSupplier(Suppliers oldSupplier, Suppliers newSupplier)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string updateStatement =
                    "UPDATE Suppliers SET " +
                    "SupName=@NewSupName " + // only supplier name can be updated
                    "WHERE SupplierId=@OldSupplierId " + // need for identification
                    "AND (SupName=@OldSupName OR " +
                    "SupName IS NULL AND @OldSupName IS NULL)";  // the AND is to ensure no one is updating this product
                using (SqlCommand cmd = new SqlCommand(updateStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    cmd.Parameters.AddWithValue("@NewSupName", newSupplier.SupName);
                    cmd.Parameters.AddWithValue("@OldSupplierId", oldSupplier.SupplierId);
                    cmd.Parameters.AddWithValue("@OldSupName", oldSupplier.SupName);

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
