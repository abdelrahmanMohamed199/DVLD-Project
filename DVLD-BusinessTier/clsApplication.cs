using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsApplication
    {
        public enum enMode { AddNew, Update };
        public enum enStatus { New = 1, Canceled = 2, Completed = 3 }

        public enum enAppType
        {
            NewLocalLicense = 1, RenewLicense = 2, ReplacementForLostLicense = 3,
            ReplacementForDamagedLicense = 4, ReleaseDetainedLicense = 5,
            NewInternationalLicense = 6, RetakeTest = 7
        }

        public enMode Mode;
        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public enStatus Status { get; set; } = enStatus.New;
        public DateTime Date { get; set; }
        public enAppType TypeID { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int UserID { get; set; }

        public clsPerson PersonInfo { get; set; }

        public clsApplication()
        {
            Mode = enMode.AddNew;
            ApplicationID = -1;
            PersonID = -1;
            Status = enStatus.New;
            Date = DateTime.Now;
            LastStatusDate = DateTime.Now;
            TypeID = enAppType.NewLocalLicense;
            PaidFees = 0;
            UserID = -1;
        }

        clsApplication(int AppID, int personID, enStatus status, DateTime date, enAppType typeID,
            DateTime lastStatusDate, decimal paidFees, int userID)
        {
            Mode = enMode.Update;
            this.ApplicationID = AppID;
            this.PersonID = personID;
            this.Status = status;
            this.Date = date;
            this.TypeID = typeID;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.UserID = userID;
            this.PersonInfo = clsPerson.Find(PersonID);
        }

        public static clsApplication GetApplication(int AppID)
        {
            int personID = -1, TypeID = -1, UserID = -1;
            byte status = 1;
            DateTime date = DateTime.Now, lastStatusDate = DateTime.Now;
            decimal paidFees = 0;
            if (clsApplicationData.GetApplicationByID(AppID, ref personID, ref date, ref TypeID,
                ref status, ref lastStatusDate, ref paidFees, ref UserID))
            {
                return new clsApplication(AppID, personID, (enStatus)status, date, (enAppType)TypeID,
                    lastStatusDate, paidFees, UserID);
            }
            return null;
        }

        bool AddApplication()
        {
            this.ApplicationID = clsApplicationData.AddApplication(this.PersonID, this.Date, (int)this.TypeID,
                 (int)this.Status, this.LastStatusDate, this.PaidFees, this.UserID);

            return this.ApplicationID != -1;
        }

        bool UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, (int)this.Status, this.LastStatusDate);
        }

        public bool SaveApplication()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return UpdateApplication();

                default:
                    return false;
            }
        }

        public static DataTable GetApplicationsList()
        {
            return clsApplicationData.GetApplicationsList();
        }

        public static bool DeleteApplication(int applicationID)
        {
            return clsApplicationData.DeleteApp(applicationID);
        }

        public static bool IsAppExist(int appID)
        {
            return clsApplicationData.IsAppExist(appID);
        }

    }
}
