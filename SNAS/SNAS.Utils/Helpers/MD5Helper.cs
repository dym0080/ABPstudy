using System;
using System.Security.Cryptography;
using System.Text;

namespace SNAS.Utils.Helpers
{
    /// <summary>
    /// MD5加密算法工具类
    /// </summary>
    public class MD5Helper
    {

        /// <summary>
        /// 对文字进行md5加密
        /// </summary>
        /// <param name="text">原文</param>
        /// <param name="codepage">文字的编码(默认utf-8)</param>
        /// <returns></returns>
        public static string EnCode(string text, string codepage = "utf-8")
        {
            byte[] result = Encoding.GetEncoding(codepage).GetBytes(text);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        public static string EnCode16Bit(string text, string codepage = "utf-8")
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.GetEncoding(codepage).GetBytes(text)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }
    }
}
