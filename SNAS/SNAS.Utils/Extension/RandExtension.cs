using System;
using System.Collections.Generic;

namespace SNAS.Utils.Extension
{
    public class RandExtension
    {
        public static int GetRandValue(int min, int max)
        {
            return new Random(GetRandomSeed()).Next(min, max);
        }

        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static string GetRandomNum()
        {
            Random ra = new Random();
            return ra.NextDouble().ToString();
        }

        /// <summary>
        /// 随机产生一个字符串
        /// </summary>
        /// <param name="includeUChar">是否包含大写字母</param>
        /// <param name="includeLChar">是否包含小写字母</param>
        /// <param name="inCludeSpec">是否包含特殊字符</param>
        /// <param name="inCludeNum">是否包含数字</param>
        /// <param name="len">字符串长度</param>
        /// <returns></returns>
        public static string GetRandomString(bool includeUChar, bool includeLChar, bool inCludeSpec, bool inCludeNum, int len)
        {
            if (!includeUChar && !includeLChar && !inCludeSpec && !inCludeNum)
                throw new Exception("至少需要包含一种字符.");
            string[] nums = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] uchars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] Lchars = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string[] specs = { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+", "{", "}", "[", "]", "|", "\\", ";", "<", ">", ",", ".", "?" };

            List<string> chars = new List<string>();
            if (includeUChar)
                chars.AddRange(uchars);
            if (includeLChar)
                chars.AddRange(Lchars);
            if (inCludeSpec)
                chars.AddRange(specs);
            if (inCludeNum)
                chars.AddRange(nums);

            int charCnt = chars.Count;
            List<string> result = new List<string>();
            for (int i = 0; i < len; i++)
            {
                result.Add(chars[GetRandValue(0, charCnt)]);
                //Thread.Sleep(50);
            }
            return string.Join("", result.ToArray());
        }
    }
}
