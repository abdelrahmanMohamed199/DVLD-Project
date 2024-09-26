using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsTestData
    {
        static public int AddTestRecord(int TestAppiontmentID, bool TestResult, string Notes, int UserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[Tests]  
                ([TestAppointmentID], [TestResult], [Notes], [CreatedByUserID])
                VALUES (@AppiontmentID, @Result, @Notes, @UserID);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppiontmentID", TestAppiontmentID);
            command.Parameters.AddWithValue("@Result", TestResult);
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
                    TestID = GeneratedID;
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
            return TestID;
        }

    }
}
