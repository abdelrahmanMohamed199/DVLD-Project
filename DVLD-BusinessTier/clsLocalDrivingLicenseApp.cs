using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsLocalDrivingLicenseApp : clsApplication
    {
        public int LocalDrivingLicenseAppID { get; set; }
        public byte LicenseClassID { get; set; }
        clsApplication ApplicationInfo { get; set; }

        public clsLocalDrivingLicenseApp() { }

        clsLocalDrivingLicenseApp(int LocalAppID, int AppID, byte ClassID)
        {
            this.LocalDrivingLicenseAppID = LocalAppID;
            this.ApplicationID = AppID;
            this.LicenseClassID = ClassID;
            this.ApplicationInfo = clsApplication.GetApplication(AppID);
            this.Mode = ApplicationInfo.Mode;
            this.PersonID = ApplicationInfo.PersonID;
            this.Status = ApplicationInfo.Status;
            this.LastStatusDate = ApplicationInfo.LastStatusDate;
            this.UserID = ApplicationInfo.UserID;
            this.Date = ApplicationInfo.Date;
            this.TypeID = ApplicationInfo.TypeID;
            this.PaidFees = ApplicationInfo.PaidFees;
            this.PersonInfo = ApplicationInfo.PersonInfo;
        }

        public byte GetNumOfPassedTests()
        {
            return clsLocalDrivingLicenseAppData.GetNumOfPassedTests(this.LocalDrivingLicenseAppID);
        }

        public static clsLocalDrivingLicenseApp GetLocalAppByID(int LocalAppID)
        {
            int AppID = -1;
            byte ClassID = 0;
            if (clsLocalDrivingLicenseAppData.GetLocalApplicationByID(LocalAppID, ref AppID, ref ClassID))
            {
                return new clsLocalDrivingLicenseApp(LocalAppID, AppID, ClassID);
            }
            return null;
        }
        public static clsLocalDrivingLicenseApp GetLocalAppByAppID(int AppID)
        {
            int LocalAppID = -1;
            byte ClassID = 0;
            if (clsLocalDrivingLicenseAppData.GetLocalApplicationByAppID(AppID, ref LocalAppID, ref ClassID))
            {
                return new clsLocalDrivingLicenseApp(LocalAppID, AppID, ClassID);
            }
            return null;
        }
        public static DataTable GetLocalAppsList()
        {
            return clsLocalDrivingLicenseAppData.GetLocalAppsList();
        }
        public bool AddLocalDrivingLicenseApp()
        {
            if(!base.SaveApplication())
                return false;

            int ID = clsLocalDrivingLicenseAppData.AddLocalDrivingLicenseApplication(ApplicationID, LicenseClassID);
            return ID != -1;
        }

        public static bool IsPersonHasLicenseOrActiveApp(int personID, byte LicenseClassID)
        {
            return clsLocalDrivingLicenseAppData.IsLocalAppExist(personID, LicenseClassID);
        }

        public static bool DeleteLocalApp(int LocalAppID)
        {
            return clsLocalDrivingLicenseAppData.DeleteLocalApp(LocalAppID);
        }
    }
}
