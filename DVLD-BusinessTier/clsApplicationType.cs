using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsApplicationType
    {
        public int AppTypeID { get; set; }
        public string AppTitle { get; set; }
        public decimal Price { get; set; }

        public clsApplicationType()
        {

        }

        clsApplicationType(int appID, string title, decimal price)
        {
            this.AppTypeID = appID;
            this.AppTitle = title;
            this.Price = price;
        }

        public static clsApplicationType Find(int AppID)
        {
            string Title = "";
            decimal Price = 0;
            if (clsApplicationTypeData.GetApptype(AppID, ref Title, ref Price))
                return new clsApplicationType(AppID, Title, Price);

            return null;
        }

        public static DataTable GetAppTypesList()
        {
            return clsApplicationTypeData.GetAppTypesList();
        }

        public bool EditApplication()
        {
            return clsApplicationTypeData.EditApplication(this.AppTypeID, this.AppTitle, this.Price);
        }
    }
}
