using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode { get; set; }
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsReleased { get; set; }
        public int ReleasedBy { get; set; }
        public int ReleaseAppID { get; set; }

        public clsDetainedLicense()
        {
            Mode = enMode.AddNew;
        }

        clsDetainedLicense(int detainID, int licenseID, DateTime detainDate, decimal fineFees,
            DateTime releaseDate, int createdBy, bool isReleased, int releasedBy, int releaseAppID)
        {
            Mode = enMode.Update;
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            ReleaseDate = releaseDate;
            CreatedBy = createdBy;
            IsReleased = isReleased;
            ReleasedBy = releasedBy;
            ReleaseAppID = releaseAppID;
        }

        public static clsDetainedLicense GetDetainedLicense(int LicenseID)
        {
            int DetainID = -1, CreatedBy = -1, ReleasedBy = -1, ReleaseAppID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = new DateTime(1, 1, 1);
            decimal FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicenseData.GetDetainedLicense(ref DetainID, LicenseID, ref DetainDate,
                ref FineFees, ref CreatedBy, ref IsReleased, ref ReleaseDate,
                ref ReleasedBy, ref ReleaseAppID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, ReleaseDate,
                    CreatedBy, IsReleased, ReleasedBy, ReleaseAppID);
            }
            return null;
        }

        bool AddDetainedLicense()
        {
            DetainID = clsDetainedLicenseData.AddDetainedLicense(LicenseID, DetainDate, FineFees,
                CreatedBy, IsReleased);

            return DetainID != -1;
        }

        bool UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(LicenseID, IsReleased,
                ReleaseDate, ReleasedBy, ReleaseAppID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return UpdateDetainedLicense();

                default:
                    return false;
            }
        }

        public static DataTable GetDetainedLicensesList()
        {
            return clsDetainedLicenseData.GetDetainedLicensesList();
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }
    }
}
