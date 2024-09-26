using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD
{
    internal class clsValidation
    {
        public static bool ValidateEmail(string email)
        {
            Regex emailRegEx = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegEx.IsMatch(email);
        }


    }
}
