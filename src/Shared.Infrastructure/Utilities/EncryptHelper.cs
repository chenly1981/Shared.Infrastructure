using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EncryptHelper
    {
        private static MD5 _md5 = MD5.Create();

        /// <summary>
        /// Encrypt string by MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptMD5(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            byte[] inputData = Encoding.UTF8.GetBytes(input);

            return EncryptMD5(inputData);
        }

        /// <summary>
        /// Encrypt the byte data by MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptMD5(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            byte[] hashData = _md5.ComputeHash(input);

            string result = BitConverter.ToString(hashData).Replace("-", "");
            return result;
        }
    }
}
