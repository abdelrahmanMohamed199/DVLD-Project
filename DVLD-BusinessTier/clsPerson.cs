using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsPerson
    {
        enum enMode { AddNew, Update }

        enMode _Mode;
        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }
        public clsCountry CountryInfo { get; set; }
        public clsPerson()
        {
            _Mode = enMode.AddNew;
            ID = -1;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            NationalNo = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CountryID = -1;
            ImagePath = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Gendor = 2;
        }

        clsPerson(enMode mode, int id, string nationalNo, string firstName, string secondName, string thirdName,
            string lastName, DateTime dateOfBirth, byte gendor, string address, string phone,
            string email, int countryID, string imagePath)
        {
            _Mode = mode;
            ID = id;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gendor = gendor;
            Address = address;
            Phone = phone;
            Email = email;
            CountryID = countryID;
            ImagePath = imagePath;
            CountryInfo = clsCountry.Find(countryID);
        }

        static public clsPerson Find(int id)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "",
            lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            byte gendor = 2;
            int countryID = 0;
            if (clsPersonData.FindByID(id, ref nationalNo, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref phone,
                ref email, ref countryID, ref imagePath))
            {
                return new clsPerson(enMode.Update, id, nationalNo, firstName, secondName, thirdName,
                    lastName, dateOfBirth, gendor, address, phone, email, countryID, imagePath);
            }
            return null;
        }

        static public clsPerson Find(string NationalNo)
        {
            int ID = 0;
            string firstName = "", secondName = "", thirdName = "",
            lastName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            byte gendor = 2;
            int countryID = 0;
            if (clsPersonData.FindByNationalNo(ref ID, NationalNo, ref firstName, ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref phone,
                ref email, ref countryID, ref imagePath))
            {
                return new clsPerson(enMode.Update, ID, NationalNo, firstName, secondName, thirdName,
                    lastName, dateOfBirth, gendor, address, phone, email, countryID, imagePath);
            }
            return null;
        }

        private bool AddPerson()
        {
            this.ID = clsPersonData.AddPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                 this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone,
                 this.Email, this.CountryID, this.ImagePath);

            return this.ID != -1;
        }

        private bool UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.ID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone,
                this.Email, this.CountryID, this.ImagePath);
        }

        static public bool DeletePerson(int personID)
        {
            return clsPersonData.DeletePerson(personID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (AddPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return UpdatePerson();

                default:
                    return false;
            }
        }

        public static DataTable GetPeopleList()
        {
            return clsPersonData.GetPeopleList();
        }

        public static bool IsPersonExist(int personID)
        {
            return clsPersonData.IsPersonExist(personID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
    }
}
