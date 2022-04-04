using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAndTree
{
    class Checker
    {

        public static bool CheckIfPrime(long number)
        {
            if (number % 2 == 0 || number % 3 == 0 || number % 5 == 0)
            {
                return false;
            }
            int limit = (int)Math.Floor(Math.Sqrt(number));
            int i = 6;
            while (i <= limit)
            {
                if (number % (i + 1) == 0 || number % (i + 5) == 0)
                {
                    return false;
                }
                i += 6;
            }
            return true;
        }
    }
}
