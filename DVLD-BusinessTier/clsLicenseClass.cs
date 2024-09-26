using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsLicenseClass
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public byte MinAge { get; set; }
        public byte ValidityLength { get; set; }
        public decimal Fees { get; set; }

        public clsLicenseClass() { }

        clsLicenseClass(int classID, string className, string description, byte minAge, byte validityLength, decimal fees)
        {
            ClassID = classID;
            ClassName = className;
            Description = description;
            MinAge = minAge;
            ValidityLength = validityLength;
            Fees = fees;
        }

        public static clsLicenseClass Find(byte ClassID)
        {
            string className = "", Description = "";
            byte MinAge = 0, ValidityLength = 0;
            decimal Fees = 0;
            if (clsLicenseClassData.GetLicenseClass(ClassID, ref className, ref Description,
                ref MinAge, ref ValidityLength, ref Fees))
            {
                return new clsLicenseClass(ClassID, className, Description, MinAge, ValidityLength, Fees);
            }
            return null;
        }

        public static DataTable GetClassesList()
        {
            return clsLicenseClassData.GetLicenseClassesList();
        }
    }
}
