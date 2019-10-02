using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorwalGeneralStore.Utility
{
    public static class RegexPattern
    {
        /// <summary>
        ///  Format            Pattern
        ///  ------------------------------------------------- 
        ///   xxxxxxxxxx      ^[0-9]{10}$
        ///   +xxxxxxxxxxxx   ^\+[0-9]{2}+[0-9]{10}$ 
        ///   +xx xx xxxxxxxx ^\+[0-9]{2}\s+[0-9]{2}\s+[0-9]{8}$ 
        ///   xxx-xxxx-xxxx   ^[0-9]{3}-[0-9]{4}-[0-9]{4}$
        /// </summary>
        public static readonly string[] mobile_number_validation_Patterns = new string[] {
            @"^[0-9]{10}$",
            @"^\+[0-9]{12}$",
            @"^\+[0-9]{2}\s+[0-9]{2}\s+[0-9]{8}$",
            @"^[0-9]{3}-[0-9]{4}-[0-9]{4}$",
        };

        public static string GetCombinedPattern(this string[] patternlist)
        {
            return string.Join("|", patternlist
              .Select(item => "(" + item + ")"));
        }
    }
}
