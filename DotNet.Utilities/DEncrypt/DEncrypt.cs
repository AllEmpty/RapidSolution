/// <summary>
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Security.Cryptography;
using System.Text;
namespace DotNet.Utilities
{
    /// <summary>
    /// Encrypt ��ժҪ˵����
    /// </summary>
    public class DEncrypt
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        public DEncrypt()
        {

        }

        #region ʹ�� ȱʡ��Կ�ַ��� ����/����string

        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������string
        /// </summary>
        /// <param name="original">����</param>
        /// <returns>����</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, "MATICSOFT");
        }
        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������string
        /// </summary>
        /// <param name="original">����</param>
        /// <returns>����</returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, "MATICSOFT", System.Text.Encoding.Default);
        }

        #endregion

        #region ʹ�� ������Կ�ַ��� ����/����string
        /// <summary>
        /// ʹ�ø�����Կ�ַ�������string
        /// </summary>
        /// <param name="original">ԭʼ����</param>
        /// <param name="key">��Կ</param>
        /// <param name="encoding">�ַ����뷽��</param>
        /// <returns>����</returns>
        public static string Encrypt(string original, string key)
        {
            byte[] buff = System.Text.Encoding.Default.GetBytes(original);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }
        /// <summary>
        /// ʹ�ø�����Կ�ַ�������string
        /// </summary>
        /// <param name="original">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Default);
        }

        /// <summary>
        /// ʹ�ø�����Կ�ַ�������string,����ָ�����뷽ʽ����
        /// </summary>
        /// <param name="encrypted">����</param>
        /// <param name="key">��Կ</param>
        /// <param name="encoding">�ַ����뷽��</param>
        /// <returns>����</returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }
        #endregion

        #region ʹ�� ȱʡ��Կ�ַ��� ����/����/byte[]
        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������byte[]
        /// </summary>
        /// <param name="encrypted">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("MATICSOFT");
            return Decrypt(encrypted, key);
        }
        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������
        /// </summary>
        /// <param name="original">ԭʼ����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("MATICSOFT");
            return Encrypt(original, key);
        }
        #endregion

        #region  ʹ�� ������Կ ����/����/byte[]

        /// <summary>
        /// ����MD5ժҪ
        /// </summary>
        /// <param name="original">����Դ</param>
        /// <returns>ժҪ</returns>
        public static byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }

        /// <summary>
        /// ʹ�ø�����Կ����
        /// </summary>
        /// <param name="original">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// ʹ�ø�����Կ��������
        /// </summary>
        /// <param name="encrypted">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        #endregion
    }
}
