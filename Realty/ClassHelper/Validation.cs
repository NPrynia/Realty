using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realty.ClassHelper
{
    public class Validation
    {
        public static bool validationText(string a)
        {
            if (string.IsNullOrWhiteSpace(a) == true)
            {
                return false;
            }

            if (!a.All(Char.IsLetter))
            {
                return false;
            }

            if (a.Contains(" "))
            {
                return false;
            }

            return true;

        }

        public static bool validationNum(string a)
        {

            if (ulong.TryParse(a, out ulong ulonga))
            {

            }
            else
            {
                return false;
            }
            return true;
        }
        
    }
}
