using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsTestType
    {
        public int TestID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public clsTestType()
        {

        }

        clsTestType(int TestID, string title, string Description, decimal price)
        {
            this.TestID = TestID;
            this.Title = title;
            this.Description = Description;
            this.Price = price;
        }

        public static clsTestType Find(int TestID)
        {
            string Title = "", Description = "";
            decimal Price = 0;
            if (clsTestTypeData.GetTesttype(TestID, ref Title, ref Description, ref Price))
                return new clsTestType(TestID, Title, Description, Price);

            return null;
        }

        public static DataTable GetTestTypesList()
        {
            return clsTestTypeData.GetTestTypesList();
        }

        public bool EditTestType()
        {
            return clsTestTypeData.EditTestType(this.TestID, this.Title, this.Description, this.Price);
        }
    }
}
