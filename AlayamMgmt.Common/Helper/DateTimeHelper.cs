using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Helper
{
    public static class DateTimeHelper
    {
        public static string MonthName(int m)
        {
            string res;
            switch (m)
            {
                case 1:
                    res = "JAN";
                    break;
                case 2:
                    res = "FEB";
                    break;
                case 3:
                    res = "MAR";
                    break;
                case 4:
                    res = "APR";
                    break;
                case 5:
                    res = "MAY";
                    break;
                case 6:
                    res = "JUN";
                    break;
                case 7:
                    res = "JUL";
                    break;
                case 8:
                    res = "AUG";
                    break;
                case 9:
                    res = "SEP";
                    break;
                case 10:
                    res = "OCT";
                    break;
                case 11:
                    res = "NOV";
                    break;
                case 12:
                    res = "DEC";
                    break;
                default:
                    res = "NULL";
                    break;
            }
            return res;
        }
    }
}
