using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StarmileFx.Common.Encryption
{
    /// <summary>
    /// 加密方法
    /// </summary>
    public class Encryption
    {
        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string toMd5(string str)
        {
            string cl = StrInCoded(str);
            string pwd = "";
            //实例化一个md5对像  
            MD5 md5 = MD5.Create();
            //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　  
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            //通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得  
            for (int i = 0; i < s.Length; i++)
            {
                //将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符   
                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }
        #endregion

        #region DES加密（解密）
        //加密密钥,要求为8位 
        private static byte[] Keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        /// <summary> 
        /// DES加密字符串 
        /// </summary> 
        /// <param name="encryptString">待加密的字符串</param> 
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns> 

        public static string toEncryptDES(string encryptString)
        {
            try
            {
                encryptString = StrInCoded(encryptString);
                byte[] rgbKey = Encoding.UTF8.GetBytes(Encoding.ASCII.GetString(Keys).Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + encryptString;
            }

        }

        /// <summary> 
        /// DES解密字符串 
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 

        public static string toDecryptDES(string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(Encoding.ASCII.GetString(Keys).Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                Byte[] inputByteArrays = new byte[inputByteArray.Length];
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return StrDeCoded(Encoding.UTF8.GetString(mStream.ToArray()));
            }
            catch (Exception ex)
            {
                return ex.Message + decryptString;
            }

        }
        #endregion 

        #region 密文排序
        /// <summary>
        /// 加密排序
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrInCoded(string str)
        {
            string s = "";
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    s += (char)(str[i] + 8);
                }
                return s;
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// 解密排序
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrDeCoded(string str)
        {
            string s = "";
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    s += (char)(str[i] - 8);
                }
                return s;
            }
            catch
            {
                return str;
            }
        }
        #endregion
    }
}
