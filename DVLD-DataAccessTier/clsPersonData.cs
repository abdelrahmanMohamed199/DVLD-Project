using System;
using System.Data.SqlClient;
using System.Data;


namespace DVLD_DataAccessTier
{
    public class clsPersonData
    {
        static public bool FindByID(int ID, ref string NationalNo, ref string fName, ref string sName,
            ref string tName, ref string lName, ref DateTime dateOfBirth, ref byte gendor,
            ref string address, ref string phone, ref string email,
            ref int countryID, ref string imagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from People where PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    fName = (string)reader["FirstName"];
                    sName = (string)reader["SecondName"];
                    tName = (string)reader["ThirdName"];
                    lName = (string)reader["LastName"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gendor = (byte)reader["Gendor"];
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];
                    countryID = (int)reader["NationalityCountryID"];
                    if (reader["Email"] != DBNull.Value)
                        email = (string)reader["Email"];
                    else
                        email = "";
                    if (reader["ImagePath"] != DBNull.Value)
                        imagePath = (string)reader["Imagepath"];
                    else
                        imagePath = "";
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

        static public bool FindByNationalNo(ref int ID, string NationalNo, ref string fName, ref string sName,
            ref string tName, ref string lName, ref DateTime dateOfBirth, ref byte gendor,
            ref string address, ref string phone, ref string email,
            ref int countryID, ref string imagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select * from People where NationalNo = @Number";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Number", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["PersonID"];
                    fName = (string)reader["FirstName"];
                    sName = (string)reader["SecondName"];
                    tName = (string)reader["ThirdName"];
                    lName = (string)reader["LastName"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gendor = (byte)reader["Gendor"];
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];
                    countryID = (int)reader["NationalityCountryID"];
                    if (reader["Email"] != DBNull.Value)
                        email = (string)reader["Email"];
                    else
                        email = "";
                    if (reader["ImagePath"] != DBNull.Value)
                        imagePath = (string)reader["Imagepath"];
                    else
                        imagePath = "";
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

        static public int AddPerson(string NationalNo, string fName, string sName,
            string tName, string lName, DateTime dateOfBirth, byte gendor,
            string address, string phone, string email,
            int countryID, string imagePath)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO [dbo].[People] ([NationalNo], [FirstName], 
                [SecondName], [ThirdName] ,[LastName] ,[Gendor],
                [Email] ,[Phone] ,[Address] ,[DateOfBirth] ,[NationalityCountryID] ,[ImagePath]) 
                VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName ,@LastName, @Gendor, 
                @Email ,@Phone ,@Address ,@DateOfBirth ,@CountryID ,@ImagePath);
                select scope_identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", fName);
            command.Parameters.AddWithValue("@SecondName", sName);
            command.Parameters.AddWithValue("@ThirdName", tName);
            command.Parameters.AddWithValue("@LastName", lName);
            command.Parameters.AddWithValue("@Gendor", gendor);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@CountryID", countryID);
            if (email != "")
            {
                command.Parameters.AddWithValue("@Email", email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            if (imagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int GeneratedID))
                {
                    PersonID = GeneratedID;
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
            return PersonID;
        }

        static public bool UpdatePerson(int ID, string NationalNo, string fName, string sName,
            string tName, string lName, DateTime dateOfBirth, byte gendor,
            string address, string phone, string email,
            int countryID, string imagePath)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE [dbo].[People]
                           SET [NationalNo] = @NationalNo 
                              ,[FirstName] = @FirstName
                              ,[SecondName] = @SecondName
                              ,[ThirdName] = @ThirdName
                              ,[LastName] = @LastName
                              ,[Email] = @Email
                              ,[Phone] = @Phone
                              ,[Address] = @Address
                              ,[DateOfBirth] = @DateOfBirth
                              ,[Gendor] = @Gendor
                              ,[NationalityCountryID] = @CountryID
                              ,[ImagePath] = @ImagePath
                         WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", fName);
            command.Parameters.AddWithValue("@SecondName", sName);
            command.Parameters.AddWithValue("@ThirdName", tName);
            command.Parameters.AddWithValue("@LastName", lName);
            command.Parameters.AddWithValue("@Gendor", gendor);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@CountryID", countryID);
            command.Parameters.AddWithValue("@PersonID", ID);
            if (email != "")
            {
                command.Parameters.AddWithValue("@Email", email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            if (imagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }

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

        static public bool DeletePerson(int PersonID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM [dbo].[People]
                             WHERE PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", PersonID);
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

        static public DataTable GetPeopleList()
        {
            DataTable dtPeopleList = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select personID, NationalNo, FirstName, SecondName, ThirdName, 
	                        LastName, DateOfBirth, Gendor,
	                        case 
		                        when Gendor = 0 then 'Male'
		                        else 'Female' 
	                        End as GendorCaption,
	                        Address, Phone, Email,  NationalityCountryID,
	                        Countries.CountryName, ImagePath
	                        from People inner join Countries 
	                        on People.NationalityCountryID = Countries.CountryID
	                        Order by People.FirstName";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtPeopleList.Load(reader);
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
            return dtPeopleList;
        }

        static public bool IsPersonExist(int personID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select FirstName from People where PersonID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", personID);
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

        static public bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "select FirstName from People where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
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
    }
}
