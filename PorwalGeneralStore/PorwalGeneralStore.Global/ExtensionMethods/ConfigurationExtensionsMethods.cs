using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Global.ExtensionMethods
{
    public static class ConfigurationExtensionsMethods
    {
        public static T BindAndReturn<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var toReturn = new T();

            configuration.GetSection(sectionName).Bind(toReturn);

            return toReturn;
        }
    }
}
