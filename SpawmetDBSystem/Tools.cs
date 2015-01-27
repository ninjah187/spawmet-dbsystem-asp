using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace SpawmetDBSystem
{
    public static class Tools
    {
        //https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5%28v=vs.110%29.aspx

        private static MD5 md5Hash = MD5.Create();
        private static StringBuilder stringBuilder = new StringBuilder();
        private static StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

        public static string GetMd5Hash(string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            stringBuilder.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);

            if (0 == stringComparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DateTime ToDateTime(this string s)
        {
            string[] date = s.Split('-');
            int year = int.Parse(date[0]);
            int month = int.Parse(date[1]);
            int day = int.Parse(date[2]);

            return new DateTime(year, month, day);
        }
    }
}