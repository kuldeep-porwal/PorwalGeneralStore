using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Utility
{
    public static class Base64
    {
        public static string Encode(
            string plainText
            )
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// we will encode data according to their type.
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Decode(
            string base64EncodedData
            )
        {
            if (string.IsNullOrEmpty(base64EncodedData))
            {
                return string.Empty;
            }
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);

            return GetEncoding(base64EncodedBytes).GetString(base64EncodedBytes);
        }

        private static Encoding GetEncoding(
            byte[] data
            )
        {
            if (data == null)
                return Encoding.Default;

            if (data.Length > 3 && data[1] == 0 && data[3] == 0)
            {
                return Encoding.Unicode;
            }

            return Encoding.UTF8;
        }
    }
}
