using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsTestAppointmentData
    {
        static public bool GetTestAppointment(int TestAppointmentID, ref int TestTypeID,
            ref int LocalAppID, ref DateTime Date, ref decimal Fees, ref int UserID, ref bool IsLocked, ref int RetakeTestAppID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from TestAppointments where TestAppointmentID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    Date = (DateTime)reader["AppointmentDate"];
                    Fees = (decimal)reader["PaidFees"];
                    UserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestAppID = (int)reader["RetakeTestApplicationID"];
                }
                reader.Close();
            }
            catch (Exception ex) { clsErrorLogger.LogError(ex.Message); }
            finally { connection.Close(); }
            return isFound;
        }
        static public bool IsTestAppointmentExist(int PersonID, int TestTypeID, byte ClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT        Found = 1
            FROM            TestAppointments INNER JOIN
            LocalDrivingLicenseApplications 
			ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID 
			INNER JOIN
            Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID INNER JOIN
            People ON Applications.ApplicantPersonID = People.PersonID
			where PersonID = @PersonID and TestTypeID = @TestTypeID and LicenseClassID = @ClassID and IsLocked != 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@ClassID", ClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    isFound = true;

                reader.Close();
            }
            catch (Exception ex) { clsErrorLogger.LogError(ex.Message); }
            finally { connection.Close(); }
            return isFound;
        }

        static public bool IsPersonPassedTest(int PersonID, int TestTypeID)
        {
            bool IsPassed = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT        1 AS Found
                            FROM            Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID INNER JOIN
                         LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID INNER JOIN
                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                            WHERE        (Applications.ApplicantPersonID = @PersonID) AND (TestAppointments.TestTypeID = @TestTypeID) AND (Tests.TestResult = 1)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    IsPassed = true;

                reader.Close();
            }
            catch (Exception ex) { clsErrorLogger.LogError(ex.Message); }
            finally { connection.Close(); }
            return IsPassed;
        }

        static public int AddTestAppointment(int TestTypeID, int LocalDrivingLicenseAppID,
            DateTime AppointmentDate, decimal PaidFees, int UserID, bool IsLocked, int RetakeTestAppID)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[TestAppointments]  
                ([TestTypeID], [LocalDrivingLicenseApplicationID], [AppointmentDate], [PaidFees],
                [CreatedByUserID], [IsLocked], [RetakeTestApplicationID])
                VALUES (@TestTypeID, @LocalAppID, @Date, @Fees, @UserID, @IsLocked, @RetakeTestAppID);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@Date", AppointmentDate);
            command.Parameters.AddWithValue("@Fees", PaidFees);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if (RetakeTestAppID <= 0)
                command.Parameters.AddWithValue("@RetakeTestAppID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestAppID", RetakeTestAppID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int GeneratedID))
                {
                    TestAppointmentID = GeneratedID;
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
            return TestAppointmentID;
        }

        static public bool EditAppointment(int AppointmentID, DateTime AppointmentDate, bool IsLocked)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE [dbo].[TestAppointments]
                           SET  [AppointmentDate] = @Date
                               ,[IsLocked] = @IsLocked
                         WHERE TestAppointmentId = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Date", AppointmentDate);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            command.Parameters.AddWithValue("@ID", AppointmentID);

            try
            {
                connection.Open();
                int RowAffected = command.ExecuteNonQuery();
                if (RowAffected > 0)
                {
                    isUpdated = true;
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
            return isUpdated;
        }

        static public DataTable GetTestAppointmentsList(int LocalAppID, int TestTypeID)
        {
            DataTable dtAppointmentsList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select TestAppointmentID, AppointmentDate, PaidFees, IsLocked
                            from TestAppointments
            where LocalDrivingLicenseApplicationID = @ID and TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtAppointmentsList.Load(reader);
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
            return dtAppointmentsList;
        }

    }
}
