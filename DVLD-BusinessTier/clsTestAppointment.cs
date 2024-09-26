using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew, Update };
        public enum enTestType { Vision = 1, Written = 2, Practical = 3 }
        public enMode Mode { get; set; }
        public int TestAppointmentID { get; set; }
        public enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseAppID { get; set; }
        public int UserID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestAppID { get; set; }

        public clsTestAppointment()
        {
            Mode = enMode.AddNew;
            AppointmentDate = DateTime.Now;
        }

        clsTestAppointment(int testAppointmentID, enTestType testTypeID, int localDrivingLicenseAppID,
            int userID, DateTime appointmentDate, decimal paidFees, bool isLocked, int retakeTestAppID)
        {
            Mode = enMode.Update;
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseAppID = localDrivingLicenseAppID;
            UserID = userID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            IsLocked = isLocked;
            RetakeTestAppID = retakeTestAppID;
        }

        public static clsTestAppointment Find(int testAppointmentID)
        {
            int TestTypeID = 0, LocalAppID = 0, UserID = 0, RetakeTestAppID = 0;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsLocked = false;
            if (clsTestAppointmentData.GetTestAppointment(testAppointmentID, ref TestTypeID, ref LocalAppID,
                ref AppointmentDate, ref PaidFees, ref UserID, ref IsLocked, ref RetakeTestAppID))
            {
                return new clsTestAppointment(testAppointmentID, (enTestType)TestTypeID,
                    LocalAppID, UserID, AppointmentDate, PaidFees, IsLocked, RetakeTestAppID);
            }
            return null;
        }

        public static bool IsTestAppointmentExist(int PersonID, int TestTypeID, byte ClassID)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(PersonID, TestTypeID, ClassID);
        }

        public static bool IsPersonPassedTest(int PersonID, int TestTypeID)
        {
            return clsTestAppointmentData.IsPersonPassedTest(PersonID, TestTypeID);
        }

        bool AddTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddTestAppointment((int)this.TestTypeID,
                this.LocalDrivingLicenseAppID, this.AppointmentDate, this.PaidFees,
                this.UserID, this.IsLocked, this.RetakeTestAppID);
            return this.TestAppointmentID != -1;
        }

        bool EditAppointment()
        {
            return clsTestAppointmentData.EditAppointment(this.TestAppointmentID, this.AppointmentDate, this.IsLocked);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return EditAppointment();

                default:
                    return false;
            }
        }
        public static DataTable GetAppointmentsList(int LocalAppID, int TestTypeID)
        {
            return clsTestAppointmentData.GetTestAppointmentsList(LocalAppID, TestTypeID);
        }

    }
}
