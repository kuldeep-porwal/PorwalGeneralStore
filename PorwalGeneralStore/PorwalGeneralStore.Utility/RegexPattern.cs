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
            //@"^\+[0-9]{12}$",
            //@"^\+[0-9]{2}\s+[0-9]{2}\s+[0-9]{8}$",
            //@"^[0-9]{3}-[0-9]{4}-[0-9]{4}$",
        };

        /// <summary>
        /// this pattern is enforce for password that contain atleast one uppercase,
        /// one lowercase, one special character and one digit 
        /// </summary>
        public static readonly string password_validation_pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";

        public static string GetCombinedPattern(this string[] patternlist)
        {
            return string.Join("|", patternlist
              .Select(item => "(" + item + ")"));
        }
    }
}
