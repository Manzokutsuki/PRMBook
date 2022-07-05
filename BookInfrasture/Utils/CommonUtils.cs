using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInfrasture.Utils
{
    public static class CommonUtils
    {
        public static string FormatStringInput(string input)
        {
            return input.ToLower().Trim();
        }
    }
}
