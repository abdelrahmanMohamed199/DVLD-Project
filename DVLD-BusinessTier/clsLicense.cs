using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsLicense
    {
        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplaceForDamage = 3, ReplaceForLost = 4 }
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public byte LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal PaidFees { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int UserID { get; set; }

        public clsLicense() { }

        clsLicense(int licenseID, int applicationID, int driverID, byte licenseClassID,
            DateTime issueDate, DateTime expirationDate, decimal paidFees, string notes,
            bool isActive, enIssueReason issueReason, int userID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            PaidFees = paidFees;
            Notes = notes;
            IsActive = isActive;
            IssueReason = issueReason;
            UserID = userID;
        }

        public static clsLicense FindByAppID(int AppID)
        {
            int LicenseID = 0, DriverID = 0, UserID = 0;
            byte LicenseClassID = 0, IssueReason = 0;
            DateTime IssueDate = DateTime.Now, ExpDate = DateTime.Now;
            decimal Fees = 0;
            string Notes = "";
            bool isActive = false;

            if (clsLicenseData.GetLicenseByAppID(AppID, ref LicenseID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpDate, ref Notes, ref Fees, ref isActive, ref IssueReason, ref UserID))
            {
                return new clsLicense(LicenseID, AppID, DriverID, LicenseClassID,
                    IssueDate, ExpDate, Fees, Notes, isActive, (enIssueReason)IssueReason, UserID);
            }
            return null;
        }

        public static clsLicense FindByID(int LicenseID)
        {
            int AppID = 0, DriverID = 0, UserID = 0;
            byte LicenseClassID = 0, IssueReason = 0;
            DateTime IssueDate = DateTime.Now, ExpDate = DateTime.Now;
            decimal Fees = 0;
            string Notes = "";
            bool isActive = false;

            if (clsLicenseData.GetLicenseByID(LicenseID, ref AppID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpDate, ref Notes, ref Fees, ref isActive, ref IssueReason, ref UserID))
            {
                return new clsLicense(LicenseID, AppID, DriverID, LicenseClassID,
                    IssueDate, ExpDate, Fees, Notes, isActive, (enIssueReason)IssueReason, UserID);
            }
            return null;
        }
        public bool IssueLicense()
        {
            LicenseID = clsLicenseData.IssueLicense(ApplicationID, DriverID,
                LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees,
                IsActive, (byte)IssueReason, UserID);
            return this.LicenseID != -1;
        }

        public bool EditLicenseActivation()
        {
            return clsLicenseData.EditLicense(this.LicenseID, this.IsActive);
        }
    }
}
