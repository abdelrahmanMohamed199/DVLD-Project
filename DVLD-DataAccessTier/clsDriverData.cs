using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsDriverData
    {
        static public bool FindByDriverID(int DriverID, ref int userID, ref int PersonID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Drivers
                            where DriverID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    userID = (int)reader["CreatedByUserID"];
                    PersonID = (int)reader["PersonID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally { connection.Close(); }
            return isFound;
        }
        static public bool FindByPersonID(int PersonID, ref int userID, ref int DriverID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Drivers
                            where PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    userID = (int)reader["CreatedByUserID"];
                    DriverID = (int)reader["DriverID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally { connection.Close(); }
            return isFound;
        }
        static public DataTable GetLocalLicenses(int DriverID)
        {
            DataTable dtDriverLocalLicensesList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, 
                            Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                            FROM            Licenses INNER JOIN
                         LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
						where DriverID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtDriverLocalLicensesList.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dtDriverLocalLicensesList;
        }
        static public DataTable GetInternationalLicenses(int DriverID)
        {
            DataTable dtDriverInterLicensesList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT InternationalLicenseID, ApplicationID, IssuedUsingLocalLicenseID, 
                            IssueDate, ExpirationDate, IsActive
                            FROM InternationalLicenses
                            where DriverID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtDriverInterLicensesList.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dtDriverInterLicensesList;
        }
        static public int AddDriver(int PersonID, int UserID, DateTime CreatedDate)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[Drivers] ([PersonID], [CreatedByUserID], [CreatedDate]) 
                VALUES (@PersonID, @UserID, @Date);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Date", CreatedDate);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int GeneratedID))
                {
                    DriverID = GeneratedID;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return DriverID;
        }
        static public DataTable GetDriversList()
        {
            DataTable dtDriversList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Drivers_View";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtDriversList.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dtDriversList;
        }
        static public bool IsDriverExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select found = 1 from Drivers where PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    isFound = true;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally { connection.Close(); }
            return isFound;
        }
        static public bool IsDriverHasNonExpLicense(int DriverID, int ClassID)
        {
            bool HasLicense = false;
            SqlConnection connection = new SqlConnection((clsDataAccessSettings.connectionString));
            string query = @"select * from Licenses
                            where DriverID = @DriverID and LicenseClass = @ClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@ClassID", ClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (DateTime.Compare((DateTime)reader["ExpirationDate"], DateTime.Now) > 0)
                    {
                        HasLicense = true;
                        break;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally { connection.Close(); }
            return HasLicense;
        }
    }
}
