using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DVLD_DataAccessTier
{
    internal class clsDataAccessSettings
    {
        static public string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
    }
}
