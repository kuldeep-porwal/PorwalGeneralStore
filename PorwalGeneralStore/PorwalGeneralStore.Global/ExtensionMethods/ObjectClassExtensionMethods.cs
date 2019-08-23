using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Global.ExtensionMethods
{
    public static class ObjectClassExtensionMethods
    {
        public static bool IsNull(
            this object Value
            )
        {
            return Value == null;
        }
        public static bool IsNotNull(
           this object Value
           )
        {
            return Value != null;
        }
    }
}
