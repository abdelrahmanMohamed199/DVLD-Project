using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string Name { get; set; }

        clsCountry(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        static public DataTable GetCountryList()
        {
            return clsCountryData.GetCountriesList();
        }

        static public clsCountry Find(string countryName)
        {
            int id = -1;
            if (clsCountryData.FindCountryByName(ref id, countryName))
            {
                return new clsCountry(id, countryName);
            }
            return null;
        }

        static public clsCountry Find(int CountryID)
        {
            string countryName = "";
            if (clsCountryData.FindCountryByID(CountryID, ref countryName))
            {
                return new clsCountry(CountryID, countryName);
            }
            return null;
        }
    }
}
