using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsLocalDrivingLicenseAppData
    {
        static public byte GetNumOfPassedTests(int ID)
        {
            byte NumOfPassedTests = 4;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select PassedTestCount from LocalDrivingLicenseApplications_View 
                            where LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && byte.TryParse(result.ToString(), out byte NewResult))
                {
                    NumOfPassedTests = NewResult;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally { connection.Close(); }
            return NumOfPassedTests;
        }

        static public bool GetLocalApplicationByAppID(int AppID, ref int LocalAppID, ref byte LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from LocalDrivingLicenseApplications 
                            where ApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", AppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LocalAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (byte)reader["LicenseClassID"];
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

        static public bool GetLocalApplicationByID(int LocalAppID, ref int AppID, ref byte LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from LocalDrivingLicenseApplications 
                            where LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalAppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    AppID = (int)reader["ApplicationID"];
                    LicenseClassID = (byte)reader["LicenseClassID"];
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

        static public DataTable GetLocalAppsList()
        {
            DataTable dtAppsList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from LocalDrivingLicenseApplications_View";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtAppsList.Load(reader);
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
            return dtAppsList;
        }

        static public bool IsLocalAppExist(int PersonID, byte LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT People.PersonID, LocalDrivingLicenseApplications.LicenseClassID,
                           Applications.ApplicationStatus
                           FROM         People INNER JOIN
                           Applications ON People.PersonID = Applications.ApplicantPersonID INNER JOIN
                           LocalDrivingLicenseApplications ON 
                           Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                           where PersonID = @PersonID and LicenseClassID = @ClassID and ApplicationStatus != 2";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ClassID", LicenseClassID);
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

        static public int AddLocalDrivingLicenseApplication(int AppID, byte LicenseClassID)
        {
            int LocalDrivingLicenseAppID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]  
                ([ApplicationID], [LicenseClassID])
                VALUES (@AppID, @ClassID);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            command.Parameters.AddWithValue("@ClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int GeneratedID))
                {
                    LocalDrivingLicenseAppID = GeneratedID;
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
            return LocalDrivingLicenseAppID;
        }

        static public bool DeleteLocalApp(int LocalAppID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM [dbo].[LocalDrivingLicenseApplications]
                             WHERE LocalDrivingLicenseApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalAppID);
            try
            {
                connection.Open();
                int AffectedRows = command.ExecuteNonQuery();
                if (AffectedRows > 0)
                    isDeleted = true;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }

    }
}
