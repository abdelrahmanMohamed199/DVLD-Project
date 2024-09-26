using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsLicenseData
    {
        static public bool GetLicenseByAppID(int AppID, ref int LicenseID, ref int DriverID,
            ref byte LicenseClassID, ref DateTime IssueDate, ref DateTime ExpDate,
            ref string Notes, ref decimal Fees, ref bool IsActive, ref byte Reason, ref int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from Licenses where ApplicationID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", AppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (byte)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";

                    Fees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    Reason = (byte)reader["IssueReason"];
                    UserID = (int)reader["CreatedByUserID"];
                    isFound = true;
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

        static public bool GetLicenseByID(int LicenseID, ref int AppID, ref int DriverID,
            ref byte LicenseClassID, ref DateTime IssueDate, ref DateTime ExpDate,
            ref string Notes, ref decimal Fees, ref bool IsActive, ref byte Reason, ref int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from Licenses where LicenseID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    AppID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (byte)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";

                    Fees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    Reason = (byte)reader["IssueReason"];
                    UserID = (int)reader["CreatedByUserID"];
                    isFound = true;
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

        static public int IssueLicense(int ApplicationID, int DriverID, byte Class,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive,
            byte IssueReason, int UserID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[Licenses] ([ApplicationID], [DriverID], [LicenseClass],
                [IssueDate], [ExpirationDate], [Notes], [PaidFees],[IsActive], 
                [IssueReason], [CreatedByUserID]) 
                VALUES (@AppID, @DriverID, @Class, @IssueDate, @ExpDate, @Notes, @Fees, @IsActive,
                @IssueReason, @UserID);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@Class", Class);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpDate", ExpirationDate);
            command.Parameters.AddWithValue("@Fees", PaidFees);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@UserID", UserID);
            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int GeneratedID))
                {
                    LicenseID = GeneratedID;
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
            return LicenseID;
        }

        static public bool EditLicense(int LicenseID, bool IsActive)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE [dbo].[Licenses]
                           SET [IsActive] = @IsActive                                                       
                         WHERE LicenseID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@ID", LicenseID);

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
    }
}
