/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace DotNet.Utilities
{
	/// <summary>
	/// ���ܽ���ʵ���ࡣ
	/// </summary>
	public class Encrypt
	{
		//��Կ
		private static byte[] arrDESKey = new byte[] {42, 16, 93, 156, 78, 4, 218, 32};
		private static byte[] arrDESIV = new byte[] {55, 103, 246, 79, 36, 99, 167, 3};

		/// <summary>
		/// ���ܡ�
		/// </summary>
		/// <param name="m_Need_Encode_String"></param>
		/// <returns></returns>
		public static string Encode(string m_Need_Encode_String)
		{
			if (m_Need_Encode_String == null)
			{
				throw new Exception("Error: \nԴ�ַ���Ϊ�գ���");
			}
			DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
			MemoryStream objMemoryStream = new MemoryStream();
			CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,objDES.CreateEncryptor(arrDESKey,arrDESIV),CryptoStreamMode.Write);
			StreamWriter objStreamWriter = new StreamWriter(objCryptoStream);
			objStreamWriter.Write(m_Need_Encode_String);
			objStreamWriter.Flush();
			objCryptoStream.FlushFinalBlock();
			objMemoryStream.Flush();
			return Convert.ToBase64String(objMemoryStream.GetBuffer(), 0, (int)objMemoryStream.Length);
		}

		/// <summary>
		/// ���ܡ�
		/// </summary>
		/// <param name="m_Need_Encode_String"></param>
		/// <returns></returns>
		public static string Decode(string m_Need_Encode_String)
		{
			if (m_Need_Encode_String == null)
			{
				throw new Exception("Error: \nԴ�ַ���Ϊ�գ���");
			}
			DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
			byte[] arrInput = Convert.FromBase64String(m_Need_Encode_String);
			MemoryStream objMemoryStream = new MemoryStream(arrInput);
			CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,objDES.CreateDecryptor(arrDESKey,arrDESIV),CryptoStreamMode.Read);
			StreamReader  objStreamReader  = new StreamReader(objCryptoStream);
			return objStreamReader.ReadToEnd();
		}

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        //public static string Md5(string encypStr)
        //{
        //    string retStr;
        //    MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
        //    byte[] inputBye;
        //    byte[] outputBye;
        //    inputBye = System.Text.Encoding.ASCII.GetBytes(encypStr);
        //    outputBye = m5.ComputeHash(inputBye);
        //    retStr = Convert.ToBase64String(outputBye);
        //    return (retStr);
        //}
        #region MD5
        /// <summary>MD5����(32bit or 16bit)</summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <param name="iType">16 or 32</param>
        /// <returns>MD5���</returns>
        public static string Md5(string str, int iType = 32)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";

            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            if (iType == 16)
            {
                ret = ret.Substring(8, 16);
            }
            return ret;
        }
        #endregion
	}
}
