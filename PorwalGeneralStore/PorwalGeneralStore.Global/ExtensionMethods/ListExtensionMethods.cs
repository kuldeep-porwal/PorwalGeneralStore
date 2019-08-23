using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Global.ExtensionMethods
{
    public static class ListExtensionMethods
    {
        public static bool IsNull<T>(
            this List<T> Value
            )
        {
            return Value == null;
        }
        public static bool IsNotNull<T>(
           this List<T> Value
           )
        {
            return Value != null;
        }
        public static bool IsNotNullAndZero<T>(
           this List<T> Value
           )
        {
            return Value != null && Value.Count == 0;
        }
        public static bool IsNotNullAndGraterThenZero<T>(
           this List<T> Value
           )
        {
            return Value != null && Value.Count > 0;
        }
    }
}
