using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsUserData
    {
        static public bool Find(ref int userID, ref int PersonID, string username,
            string password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Users
                            where UserName = @Username and Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex) { clsErrorLogger.LogError(ex.Message); }
            finally { connection.Close(); }
            return isFound;
        }

        static public bool FindByUserID(int userID, ref int PersonID, ref string username,
            ref string password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select * from Users
                            where UserID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", userID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    username = (string)reader["UserName"];
                    password = (string)reader["Password"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }
            catch (Exception ex) { clsErrorLogger.LogError(ex.Message); }
            finally { connection.Close(); }
            return isFound;
        }

        static public int AddUser(int PersonID, string Username, string Password, bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[Users] ([PersonID], [UserName], [Password], [IsActive]) 
                VALUES (@PersonID, @Username, @Password, @IsActive);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int GeneratedID))
                {
                    UserID = GeneratedID;
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
            return UserID;
        }

        static public bool UpdateUser(int UserID, string Username, string Password, bool IsActive)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE [dbo].[Users]
                           SET [UserName] = @Username 
                              ,[Password] = @Password
                              ,[IsActive] = @IsActive                             
                         WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        static public bool DeleteUser(int UserID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM [dbo].[Users]
                             WHERE UserID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", UserID);
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

        static public DataTable GetUsersList()
        {
            DataTable dtUsersList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT        Users.UserID, Users.PersonID, 
                            FullName = FirstName + ' ' + SecondName + ' ' + ThirdName + ' ' + LastName, 
                            Users.UserName, Users.IsActive
                            FROM            People INNER JOIN
                            Users ON People.PersonID = Users.PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtUsersList.Load(reader);
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
            return dtUsersList;
        }

        static public bool IsUserExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select found = 1 from Users where PersonID = @ID";
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
            catch (Exception ex) { clsErrorLogger.LogError(ex.Message); }
            finally { connection.Close(); }
            return isFound;
        }

        static public bool IsUserExist(string Username)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select PersonID from Users where UserName = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", Username);
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

        static public bool ChangePassword(int UserID, string Password)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE [dbo].[Users]
                           SET  [Password] = @Password                            
                         WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserID", UserID);

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
