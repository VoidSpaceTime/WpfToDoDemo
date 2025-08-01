﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string GetMD5(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException("Input string cannot be null or empty.", nameof(data));
            }
            var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(data));
            return Convert.ToBase64String(hash);
        }
    }
}
