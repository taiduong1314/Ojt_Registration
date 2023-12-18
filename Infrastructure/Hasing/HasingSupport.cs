using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hasing
{
    public class HasingSupport : IHasingSupport
    {
        public string encriptSHA256(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public string parseEndDate(string endDate)
        {
            string month = endDate.Trim().Substring(0, 2);
            string date = endDate.Trim().Substring(3, 2);
            string year = endDate.Trim().Substring(6, 4);
            string time = endDate.Trim().Substring(11);
            return year + "-" + month + "-" + date + " " + time;
        }
    }
}
