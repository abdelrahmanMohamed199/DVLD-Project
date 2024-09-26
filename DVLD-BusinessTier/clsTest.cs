using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsTest
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public int UserID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }

        public bool AddTestRecord()
        {
            this.TestID = clsTestData.AddTestRecord(this.TestAppointmentID, this.TestResult,
                this.Notes, this.UserID);
            return this.TestID != -1;
        }

    }
}
