using System;
using System.Linq;

namespace Shared.Core.Extensions
{
    public static class RandomExtension
    {
        public static string RandomString(this Random me, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[me.Next(s.Length)]).ToArray());
        }
    }
}