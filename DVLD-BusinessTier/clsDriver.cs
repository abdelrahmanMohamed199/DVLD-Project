using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsDriver() { }

        clsDriver(int driverID, int personID, int userID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            UserID = userID;
            CreatedDate = createdDate;
            PersonInfo = clsPerson.Find(personID);
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = 0, UserID = 0;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.FindByDriverID(DriverID, ref UserID, ref PersonID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, UserID, CreatedDate);
            }
            return null;
        }

        public static clsDriver FindByPersonID(int personID)
        {
            int DriverID = 0, UserID = 0;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.FindByPersonID(personID, ref UserID, ref DriverID, ref CreatedDate))
            {
                return new clsDriver(DriverID, personID, UserID, CreatedDate);
            }
            return null;
        }

        public DataTable GetLocalLicenses()
        {
            return clsDriverData.GetLocalLicenses(this.DriverID);
        }

        public DataTable GetInternationalLicenses()
        {
            return clsDriverData.GetInternationalLicenses(this.DriverID);
        }

        public bool AddDriver()
        {
            this.DriverID = clsDriverData.AddDriver(this.PersonID, this.UserID, this.CreatedDate);
            return this.DriverID != -1;
        }

        public static DataTable GetDriversList()
        {
            return clsDriverData.GetDriversList();
        }

        public static bool IsDriverExist(int PersonID)
        {
            return clsDriverData.IsDriverExist(PersonID);
        }

        public static bool IsDriverHasNonExpLicense(int DriverID, int ClassID)
        {
            return clsDriverData.IsDriverHasNonExpLicense(DriverID, ClassID);
        }
    }
}
