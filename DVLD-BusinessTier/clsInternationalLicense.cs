using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsInternationalLicense : clsApplication
    {
        public int InterLicenseID { get; set; }
        public int DriverID { get; set; }
        public int LocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        clsApplication AppInfo { get; set; }

        public clsInternationalLicense() { }

        clsInternationalLicense(int interLicenseID, int AppID, int driverID, int localLicenseID,
            DateTime issueDate, DateTime expirationDate, bool isActive, int UserID)
        {
            this.ApplicationID = AppID;
            InterLicenseID = interLicenseID;
            DriverID = driverID;
            LocalLicenseID = localLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            this.UserID = UserID;
            this.Mode = enMode.Update;
            AppInfo = clsApplication.GetApplication(AppID);
            this.PersonID = AppInfo.PersonID;
            this.Date = AppInfo.Date;
            this.PaidFees = AppInfo.PaidFees;
            this.LastStatusDate = AppInfo.LastStatusDate;
            this.Status = AppInfo.Status;
            this.TypeID = AppInfo.TypeID;
            this.PersonInfo = AppInfo.PersonInfo;
        }

        public static clsInternationalLicense GetInternationalLicense(int InternationalLicenseID)
        {
            int AppID = 0, DriverID = 0, LocalLicenseID = 0, UserID = 0;
            DateTime IssueDate = DateTime.Now, ExpDate = DateTime.Now;
            bool IsActive = false;
            if (clsInternationalLicenseData.GetInternationalLicense(InternationalLicenseID, ref AppID,
                ref DriverID, ref LocalLicenseID, ref IssueDate, ref ExpDate, ref IsActive, ref UserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, AppID, DriverID,
                    LocalLicenseID, IssueDate, ExpDate, IsActive, UserID);
            }
            return null;
        }

        public static clsInternationalLicense GetInterlLicenseByDriverID(int DriverID)
        {
            int AppID = 0, InternationalLicenseID = 0, LocalLicenseID = 0, UserID = 0;
            DateTime IssueDate = DateTime.Now, ExpDate = DateTime.Now;
            bool IsActive = false;
            if (clsInternationalLicenseData.GetInternationalLicenseByDriverID(ref InternationalLicenseID,
                ref AppID, DriverID, ref LocalLicenseID, ref IssueDate, ref ExpDate,
                ref IsActive, ref UserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, AppID, DriverID,
                    LocalLicenseID, IssueDate, ExpDate, IsActive, UserID);
            }
            return null;
        }
        public bool IssueLicense()
        {
            if(!base.SaveApplication())
                return false;

            this.InterLicenseID = clsInternationalLicenseData.IssueLicense(this.ApplicationID, this.DriverID,
                this.LocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.UserID);

            return this.InterLicenseID != -1;
        }

        public static DataTable GetInterAppsList()
        {
            return clsInternationalLicenseData.GetInternationalAppsList();
        }

        public static bool IsDriverHasInterLicense(int DriverID)
        {
            return clsInternationalLicenseData.IsDriverHasInterLicense(DriverID);
        }
    }
}
