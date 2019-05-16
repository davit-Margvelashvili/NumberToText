using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberToText
{
    public static class NumberToTextConverter
    {
        static readonly Dictionary<int, string> digits = new Dictionary<int, string>
        {
             [1] = "ერთი",
             [2] = "ორი",
             [3] = "სამი",
             [4] = "ოთხი",
             [5] = "ხუთი",
             [6] = "ექვსი",
             [7] = "შვიდი",
             [8] = "რვა",
             [9] = "ცხრა",
        };

        public static string Convert(double number)
        {
            int wholePart = (int)number;
            return digits[wholePart];
        }


    }
}
