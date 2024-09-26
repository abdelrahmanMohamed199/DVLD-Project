using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsDetainedLicenseData
    {
        static public bool GetDetainedLicense(ref int DetainedLicenseID, int LicenseID, ref DateTime Date,
            ref decimal FineFees, ref int CreatedBy, ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedBy, ref int ReleaseAppID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Select * from DetainedLicenses
                            where LicenseID = @ID and IsReleased = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Date = (DateTime)reader["DetainDate"];
                    DetainedLicenseID = (int)reader["DetainID"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedBy = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    if (reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedBy = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseAppID = (int)reader["ReleaseApplicationID"];

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

        static public int AddDetainedLicense(int LicenseID, DateTime Date, decimal FineFees,
            int CreatedBy, bool IsReleased)
        {
            int DetainID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[DetainedLicenses] ([LicenseID], [DetainDate], [FineFees],
                [CreatedByUserID], [IsReleased], [ReleaseDate], [ReleasedByUserID], [ReleaseApplicationID]) 
                VALUES (@LicenseID, @Date, @Fees, @CreatedBy, @IsReleased, @ReleaseDate, 
                @ReleasedBy, @AppID);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@Date", Date);
            command.Parameters.AddWithValue("@Fees", FineFees);
            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            command.Parameters.AddWithValue("@ReleasedBy", DBNull.Value);
            command.Parameters.AddWithValue("@AppID", DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int GeneratedID))
                {
                    DetainID = GeneratedID;
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
            return DetainID;
        }

        static public bool UpdateDetainedLicense(int LicenseID, bool IsReleased,
            DateTime ReleaseDate, int ReleasedBy, int ReleaseAppID)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE [dbo].[DetainedLicenses]
                           SET [IsReleased] = @IsReleased 
                              ,[ReleaseDate] = @ReleaseDate
                              ,[ReleasedByUserID] = @ReleasedBy
                              ,[ReleaseApplicationID] = @AppID
                         WHERE LicenseID = @ID and IsReleased = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ID", LicenseID);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedBy", ReleasedBy);
            command.Parameters.AddWithValue("@AppID", ReleaseAppID);

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

        static public DataTable GetDetainedLicensesList()
        {
            DataTable dtDetainedLicensesList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT  DetainedLicenses.DetainID, DetainedLicenses.LicenseID, 
                           DetainedLicenses.DetainDate, DetainedLicenses.IsReleased, 
                        DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, People.NationalNo, 
						FullName = People.FirstName + People.SecondName + People.ThirdName + People.LastName,
                        DetainedLicenses.ReleaseApplicationID
                        FROM    Drivers INNER JOIN
                         People ON Drivers.PersonID = People.PersonID INNER JOIN
                         Licenses ON Drivers.DriverID = Licenses.DriverID INNER JOIN
                         DetainedLicenses ON Licenses.LicenseID = DetainedLicenses.LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtDetainedLicensesList.Load(reader);
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
            return dtDetainedLicensesList;
        }

        static public bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select found = 1 from DetainedLicenses 
                            where LicenseID = @ID and IsReleased = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    IsDetained = true;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally { connection.Close(); }
            return IsDetained;
        }
    }
}
