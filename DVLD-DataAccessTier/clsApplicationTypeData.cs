using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsApplicationTypeData
    {
        static public bool GetApptype(int AppID, ref string Title, ref decimal Price)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from ApplicationTypes where ApplicationTypeID = @AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    Title = (string)reader["ApplicationTypeTitle"];
                    Price = (decimal)reader["ApplicationFees"];
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
            }
            finally { connection.Close(); }
            return isFound;
        }
        static public DataTable GetAppTypesList()
        {
            DataTable dtAppsList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from ApplicationTypes";
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

        static public bool EditApplication(int AppID, string AppTitle, decimal Price)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE [dbo].[ApplicationTypes]
                           SET [ApplicationTypeTitle] = @AppTitle
                              ,[ApplicationFees] = @Price                           
                         WHERE ApplicationTypeID = @AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppTitle", AppTitle);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@AppID", AppID);

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
