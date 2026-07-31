using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry
{
    internal class ToStringData
    {
        public static string DateFullToString(DateTime date)
        {
            String monthString = "";
            switch (date.Month)
            {
                case 1: monthString = "января"; break;
                case 2: monthString = "февраля"; break;
                case 3: monthString = "марта"; break;
                case 4: monthString = "апреля"; break;
                case 5: monthString = "мая"; break;
                case 6: monthString = "июня"; break;
                case 7: monthString = "июля"; break;
                case 8: monthString = "августа"; break;
                case 9: monthString = "сентября"; break;
                case 10: monthString = "октября"; break;
                case 11: monthString = "ноября"; break;
                case 12: monthString = "декабря"; break;
            }
            return "«" + date.Day + "» " + monthString + " " + date.Year + " г.";
        }
    }
}
