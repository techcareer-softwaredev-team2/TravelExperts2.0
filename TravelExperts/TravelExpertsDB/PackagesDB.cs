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
        // get all Packages 
        public static List<Packages> GetPackages()
        {
            List<Packages> packages = new List<Packages>(); // empty list of packages

            //create the connection
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create select command
                string query = "SELECT PackageId, PkgName, PkgStartDate, PkgEndDate, " +
                               "PkgDesc,PkgBasePrice, PkgAgencyCommission " +
                               "FROM Packages " +
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
                        Packages pkg = new Packages(); // for reading
                        pkg.PackageId = (int)reader["PackageId"];
                        pkg.PkgName = reader["PkgName"].ToString();

                        int col_std = reader.GetOrdinal("PkgStartDate"); //column number of Start Date
                        if (reader.IsDBNull(col_std)) // if reader contains DBNull in this column
                            pkg.PkgStartDate = null; // make it null in the object
                        else // it is not null
                            pkg.PkgStartDate = (DateTime)reader["PkgStartDate"];

                        int col_end = reader.GetOrdinal("PkgEndDate"); //column number of End Date
                        if (reader.IsDBNull(col_end)) // if reader contains DBNull in this column
                            pkg.PkgEndDate = null; // make it null in the object
                        else // it is not null
                            pkg.PkgEndDate = (DateTime)reader["PkgEndDate"];

                        pkg.PkgDesc = reader["PkgDesc"].ToString();
                        pkg.PkgBasePrice = (Decimal)reader["PkgBasePrice"];
                        pkg.PkgAgencyCommission = (Decimal)reader["PkgAgencyCommission"];
                        packages.Add(pkg);
                    }
                } // command object recycled
            } // connection object recyled
            return packages;
        }

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
                               "PkgDesc, PkgBasePrice, PkgAgencyCommission " +
                               "FROM Packages " +
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
                            package.PkgEndDate = Convert.ToDateTime(reader["PkgEndDate"]);

                        package.PkgDesc = reader["PkgDesc"].ToString();
                        package.PkgBasePrice = Convert.ToDecimal(reader["PkgBasePrice"]);
                        package.PkgAgencyCommission = Convert.ToDecimal(reader["PkgAgencyCommission"]);

                    }
                } // command object recycled
            } // connection object recyled
            return package;
        }

        // insert a new package into Packages table
        // return new package Id
        public static int AddPackage(Packages package)
        {
            int packageId = 0;
            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                // create INSERT command
                // CustomerID is IDENTITY so no value provided
                string insertStatement = "INSERT INTO Packages(PkgName, PkgStartDate, "+
                                         " PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission) " +
                                         "OUTPUT inserted.PackageId " +
                                         "VALUES(@PkgName, @PkgStartDate, @PkgEndDate, @PkgDesc, @PkgBasePrice, @PkgAgencyCommission)";

                using (SqlCommand cmd = new SqlCommand(insertStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                   
                    cmd.Parameters.AddWithValue("@PkgName", package.PkgName);

                    if (package.PkgStartDate == null)
                        cmd.Parameters.AddWithValue("@PkgStartDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@PkgStartDate", (DateTime)package.PkgStartDate);

                    if (package.PkgEndDate == null)
                        cmd.Parameters.AddWithValue("@PkgEndDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@PkgEndDate", (DateTime)package.PkgEndDate);

                    cmd.Parameters.AddWithValue("@PkgDesc", package.PkgDesc);
                    cmd.Parameters.AddWithValue("@PkgBasePrice", package.PkgBasePrice);

                    if(package.PkgAgencyCommission.Equals(null))
                        cmd.Parameters.AddWithValue("@PkgAgencyCommission", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@PkgAgencyCommission", package.PkgAgencyCommission);

                    // execute INSERT command
                    try
                    {
                        // open the connection
                        connection.Open();
                        // execute insert command and get inserted ID
                        packageId = (int)cmd.ExecuteScalar();
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
            return packageId;
        }

        // delete a package from Packages table
        // return indicator of success
        public static bool DeletePackage(Packages package)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string deleteStatement1 =
                   "DELETE FROM Packages_Products_Suppliers " +
                   "WHERE PackageId=@PackageId ";

                string deleteStatement2 =
                    "DELETE FROM Packages " +
                    "WHERE PackageId=@PackageId " + // need for identification
                    "AND PkgName=@PkgName " +
                    "AND (PkgStartDate=@PkgStartDate OR PkgStartDate IS NULL AND @PkgStartDate IS NULL) " +
                    "AND (PkgEndDate=@PkgEndDate OR PkgEndDate IS NULL AND @PkgEndDate IS NULL) " +
                    "AND (PkgDesc = @PkgDesc OR PkgDesc IS NULL AND @PkgDesc IS NULL) " +
                    "AND PkgBasePrice=@PkgBasePrice  " +
                    "AND (PkgAgencyCommission=@PkgAgencyCommission OR PkgAgencyCommission IS NULL AND @PkgAgencyCommission IS NULL) ";

                connection.Open();
                // start a location transaction
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction
                SqlCommand cmd = connection.CreateCommand();
                cmd.Transaction = sqlTran;
                //supply paramter value, this way can avoid sql injection problem
                cmd.Parameters.AddWithValue("@PackageId", package.PackageId);
                cmd.Parameters.AddWithValue("@PkgName", package.PkgName);

                if (package.PkgStartDate == null)
                    cmd.Parameters.AddWithValue("@PkgStartDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@PkgStartDate", package.PkgStartDate);

                if (package.PkgEndDate == null)
                    cmd.Parameters.AddWithValue("@PkgEndDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@PkgEndDate", package.PkgEndDate);

                cmd.Parameters.AddWithValue("@PkgDesc", package.PkgDesc);

                cmd.Parameters.AddWithValue("@PkgBasePrice", package.PkgBasePrice);

                if (package.PkgAgencyCommission.Equals(null))
                    cmd.Parameters.AddWithValue("@PkgAgencyCommission", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@PkgAgencyCommission", package.PkgAgencyCommission);

                try
                {
                    // Execute three separate commands
                    cmd.CommandText = deleteStatement1;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = deleteStatement2;
                    cmd.ExecuteNonQuery();

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

        // Update Package: oldPackage - before update, newPackage - new data
        public static bool UpdatePackage(Packages oldPackage, Packages newPackage)
        {
            bool success = false;

            // create the connection 
            using (SqlConnection connection = TravelExpertsDB.GetConnection())
            {
                string updateStatement =
                    "UPDATE Packages SET " +
                    "PkgName=@NewPkgName, " +
                    "PkgStartDate=@NewPkgStartDate, " +
                    "PkgEndDate=@NewPkgEndDate, " +
                    "PkgDesc=@NewPkgDesc, " +
                    "PkgBasePrice=@NewPkgBasePrice, " +
                    "PkgAgencyCommission=@NewPkgAgencyCommission " +
                    "WHERE PackageId=@OldPackageId " + // need for identification
                    "AND PkgName=@OldPkgName " +
                    "AND (PkgStartDate=@OldPkgStartDate OR PkgStartDate IS NULL AND @OldPkgStartDate IS NULL) " +
                    "AND (PkgEndDate=@OldPkgEndDate OR PkgEndDate IS NULL AND @OldPkgEndDate IS NULL) " +
                    "AND (PkgDesc=@OldPkgDesc OR PkgDesc IS NULL AND @OldPkgDesc IS NULL) " +
                    "AND PkgBasePrice=@OldPkgBasePrice " +
                    "AND (PkgAgencyCommission = @OldPkgAgencyCommission OR PkgAgencyCommission IS NULL AND @OldPkgAgencyCommission IS NULL) ";  
                using (SqlCommand cmd = new SqlCommand(updateStatement, connection))
                {
                    //supply paramter value, this way can avoid sql injection problem
                    cmd.Parameters.AddWithValue("@OldPackageId", oldPackage.PackageId);

                    cmd.Parameters.AddWithValue("@NewPkgName", newPackage.PkgName);
                    cmd.Parameters.AddWithValue("@NewPkgDesc", newPackage.PkgDesc);
                    cmd.Parameters.AddWithValue("@NewPkgBasePrice", newPackage.PkgBasePrice);

                    if (newPackage.PkgStartDate == null)
                        cmd.Parameters.AddWithValue("@NewPkgStartDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@NewPkgStartDate", (DateTime)newPackage.PkgStartDate);

                    if (newPackage.PkgEndDate == null)
                        cmd.Parameters.AddWithValue("@NewPkgEndDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@NewPkgEndDate", (DateTime)newPackage.PkgEndDate);

                    if (newPackage.PkgAgencyCommission.Equals(null))
                        cmd.Parameters.AddWithValue("@NewPkgAgencyCommission", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@NewPkgAgencyCommission", Convert.ToDecimal(newPackage.PkgAgencyCommission));


                    cmd.Parameters.AddWithValue("@OldPkgName", oldPackage.PkgName);
                    cmd.Parameters.AddWithValue("@OldPkgDesc", oldPackage.PkgDesc);
                    cmd.Parameters.AddWithValue("@OldPkgBasePrice", oldPackage.PkgBasePrice);

                    if (oldPackage.PkgStartDate == null)
                        cmd.Parameters.AddWithValue("@OldPkgStartDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@OldPkgStartDate", (DateTime)oldPackage.PkgStartDate);

                    if (oldPackage.PkgEndDate == null)
                        cmd.Parameters.AddWithValue("@OldPkgEndDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@OldPkgEndDate", (DateTime)oldPackage.PkgEndDate);

                    if (oldPackage.PkgAgencyCommission.Equals(null))
                        cmd.Parameters.AddWithValue("@OldPkgAgencyCommission", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@OldPkgAgencyCommission", Convert.ToDecimal(oldPackage.PkgAgencyCommission));

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

