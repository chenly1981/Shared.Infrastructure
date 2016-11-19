using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StringHelper
    {
        static Random rnd = new Random();

        /// <summary>
        /// Get random string with given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            char[] code = "abcdefghjklmnpqrstuvwxyz23456789".ToCharArray();

            char[] result = new char[length];

            for (var i = 0; i < length; i++)
            {
                var chr = code[rnd.Next(code.Length)];
                result[i] = chr;
            }

            return new string(result);
        }
    }
}
