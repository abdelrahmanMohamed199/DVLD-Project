using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsUser
    {
        enum enMode { AddNew, Update };

        enMode _Mode;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsUser()
        {
            _Mode = enMode.AddNew;
            UserID = -1;
            Username = "";
            Password = "";
            IsActive = false;
        }

        clsUser(enMode Mode, int userID, int personID, string username, string password, bool isActive)
        {
            _Mode = Mode;
            UserID = userID;
            PersonID = personID;
            Username = username;
            Password = password;
            IsActive = isActive;
            PersonInfo = clsPerson.Find(personID);
        }

        public static clsUser Find(string Username, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool IsActive = false;
            if (clsUserData.Find(ref UserID, ref PersonID, Username, Password, ref IsActive))
            {
                return new clsUser(enMode.Update, UserID, PersonID, Username, Password, IsActive);
            }
            return null;
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string Username = "", Password = "";
            bool IsActive = false;
            if (clsUserData.FindByUserID(UserID, ref PersonID, ref Username, ref Password, ref IsActive))
            {
                return new clsUser(enMode.Update, UserID, PersonID, Username, Password, IsActive);
            }
            return null;
        }

        private bool AddUser()
        {
            this.UserID = clsUserData.AddUser(this.PersonID, this.Username, this.Password, this.IsActive);
            return UserID != -1;
        }

        private bool UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.Username, this.Password, this.IsActive);
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (AddUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return UpdateUser();

                default:
                    return false;
            }
        }

        public static DataTable GetUsersList()
        {
            return clsUserData.GetUsersList();
        }

        public static bool IsUserExist(string Username)
        {
            return clsUserData.IsUserExist(Username);
        }

        public static bool IsUserExist(int personID)
        {
            return clsUserData.IsUserExist(personID);
        }

        public bool ChangePassword(string Password)
        {
            return clsUserData.ChangePassword(this.UserID, Password);
        }

    }
}
