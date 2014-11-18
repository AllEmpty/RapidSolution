/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System; 
using System.Text; 
using System.Security.Cryptography;
namespace DotNet.Utilities
{ 
	/// <summary> 
	/// RSA���ܽ��ܼ�RSAǩ������֤
	/// </summary> 
	public class RSACryption 
	{ 		
		public RSACryption() 
		{ 			
		} 
		

		#region RSA ���ܽ��� 

		#region RSA ����Կ���� 
	
		/// <summary>
		/// RSA ����Կ���� ����˽Կ �͹�Կ 
		/// </summary>
		/// <param name="xmlKeys"></param>
		/// <param name="xmlPublicKey"></param>
		public void RSAKey(out string xmlKeys,out string xmlPublicKey) 
		{ 			
				System.Security.Cryptography.RSACryptoServiceProvider rsa=new RSACryptoServiceProvider(); 
				xmlKeys=rsa.ToXmlString(true); 
				xmlPublicKey = rsa.ToXmlString(false); 			
		} 
		#endregion 

		#region RSA�ļ��ܺ��� 
		//############################################################################## 
		//RSA ��ʽ���� 
		//˵��KEY������XML����ʽ,���ص����ַ��� 
		//����һ����Ҫ˵�������ü��ܷ�ʽ�� ���� ���Ƶģ��� 
		//############################################################################## 

		//RSA�ļ��ܺ���  string
		public string RSAEncrypt(string xmlPublicKey,string m_strEncryptString ) 
		{ 
			
			byte[] PlainTextBArray; 
			byte[] CypherTextBArray; 
			string Result; 
			RSACryptoServiceProvider rsa=new RSACryptoServiceProvider(); 
			rsa.FromXmlString(xmlPublicKey); 
			PlainTextBArray = (new UnicodeEncoding()).GetBytes(m_strEncryptString); 
			CypherTextBArray = rsa.Encrypt(PlainTextBArray, false); 
			Result=Convert.ToBase64String(CypherTextBArray); 
			return Result; 
			
		} 
		//RSA�ļ��ܺ��� byte[]
		public string RSAEncrypt(string xmlPublicKey,byte[] EncryptString ) 
		{ 
			
			byte[] CypherTextBArray; 
			string Result; 
			RSACryptoServiceProvider rsa=new RSACryptoServiceProvider(); 
			rsa.FromXmlString(xmlPublicKey); 
			CypherTextBArray = rsa.Encrypt(EncryptString, false); 
			Result=Convert.ToBase64String(CypherTextBArray); 
			return Result; 
			
		} 
		#endregion 

		#region RSA�Ľ��ܺ��� 
		//RSA�Ľ��ܺ���  string
		public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString ) 
		{			
			byte[] PlainTextBArray; 
			byte[] DypherTextBArray; 
			string Result; 
			System.Security.Cryptography.RSACryptoServiceProvider rsa=new RSACryptoServiceProvider(); 
			rsa.FromXmlString(xmlPrivateKey); 
			PlainTextBArray =Convert.FromBase64String(m_strDecryptString); 
			DypherTextBArray=rsa.Decrypt(PlainTextBArray, false); 
			Result=(new UnicodeEncoding()).GetString(DypherTextBArray); 
			return Result; 
			
		} 

		//RSA�Ľ��ܺ���  byte
		public string RSADecrypt(string xmlPrivateKey, byte[] DecryptString ) 
		{			
			byte[] DypherTextBArray; 
			string Result; 
			System.Security.Cryptography.RSACryptoServiceProvider rsa=new RSACryptoServiceProvider(); 
			rsa.FromXmlString(xmlPrivateKey); 
			DypherTextBArray=rsa.Decrypt(DecryptString, false); 
			Result=(new UnicodeEncoding()).GetString(DypherTextBArray); 
			return Result; 
			
		} 
		#endregion 

		#endregion 

		#region RSA����ǩ�� 

		#region ��ȡHash������ 
		//��ȡHash������ 
		public bool GetHash(string m_strSource, ref byte[] HashData) 
		{ 			
			//���ַ�����ȡ��Hash���� 
			byte[] Buffer; 
			System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5"); 
			Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource); 
			HashData = MD5.ComputeHash(Buffer); 

			return true; 			
		} 

		//��ȡHash������ 
		public bool GetHash(string m_strSource, ref string strHashData) 
		{ 
			
			//���ַ�����ȡ��Hash���� 
			byte[] Buffer; 
			byte[] HashData; 
			System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5"); 
			Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource); 
			HashData = MD5.ComputeHash(Buffer); 

			strHashData = Convert.ToBase64String(HashData); 
			return true; 
			
		} 

		//��ȡHash������ 
		public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData) 
		{ 
			
			//���ļ���ȡ��Hash���� 
			System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5"); 
			HashData = MD5.ComputeHash(objFile); 
			objFile.Close(); 

			return true; 
			
		} 

		//��ȡHash������ 
		public bool GetHash(System.IO.FileStream objFile, ref string strHashData) 
		{ 
			
			//���ļ���ȡ��Hash���� 
			byte[] HashData; 
			System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5"); 
			HashData = MD5.ComputeHash(objFile); 
			objFile.Close(); 

			strHashData = Convert.ToBase64String(HashData); 

			return true; 
			
		} 
		#endregion 

		#region RSAǩ�� 
		//RSAǩ�� 
		public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData) 
		{ 
			
				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPrivate); 
				System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA); 
				//����ǩ�����㷨ΪMD5 
				RSAFormatter.SetHashAlgorithm("MD5"); 
				//ִ��ǩ�� 
				EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature); 

				return true; 
			
		} 

		//RSAǩ�� 
		public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData) 
		{ 
			
				byte[] EncryptedSignatureData; 

				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPrivate); 
				System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA); 
				//����ǩ�����㷨ΪMD5 
				RSAFormatter.SetHashAlgorithm("MD5"); 
				//ִ��ǩ�� 
				EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature); 

				m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData); 

				return true; 
			
		} 

		//RSAǩ�� 
		public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData) 
		{ 
			
				byte[] HashbyteSignature; 

				HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature); 
				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPrivate); 
				System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA); 
				//����ǩ�����㷨ΪMD5 
				RSAFormatter.SetHashAlgorithm("MD5"); 
				//ִ��ǩ�� 
				EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature); 

				return true; 
			
		} 

		//RSAǩ�� 
		public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData) 
		{ 
			
				byte[] HashbyteSignature; 
				byte[] EncryptedSignatureData; 

				HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature); 
				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPrivate); 
				System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA); 
				//����ǩ�����㷨ΪMD5 
				RSAFormatter.SetHashAlgorithm("MD5"); 
				//ִ��ǩ�� 
				EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature); 

				m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData); 

				return true; 
			
		} 
		#endregion 

		#region RSA ǩ����֤ 

		public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData) 
		{ 
			
				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPublic); 
				System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA); 
				//ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
				RSADeformatter.SetHashAlgorithm("MD5"); 

				if(RSADeformatter.VerifySignature(HashbyteDeformatter,DeformatterData)) 
				{ 
					return true; 
				} 
				else 
				{ 
					return false; 
				} 
			
		} 

		public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, byte[] DeformatterData) 
		{ 
			
				byte[] HashbyteDeformatter; 

				HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter); 

				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPublic); 
				System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA); 
				//ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
				RSADeformatter.SetHashAlgorithm("MD5"); 

				if(RSADeformatter.VerifySignature(HashbyteDeformatter,DeformatterData)) 
				{ 
					return true; 
				} 
				else 
				{ 
					return false; 
				} 
			
		} 

		public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, string p_strDeformatterData) 
		{ 
			
				byte[] DeformatterData; 

				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPublic); 
				System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA); 
				//ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
				RSADeformatter.SetHashAlgorithm("MD5"); 

				DeformatterData =Convert.FromBase64String(p_strDeformatterData); 

				if(RSADeformatter.VerifySignature(HashbyteDeformatter,DeformatterData)) 
				{ 
					return true; 
				} 
				else 
				{ 
					return false; 
				} 
			
		} 

		public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData) 
		{ 
			
				byte[] DeformatterData; 
				byte[] HashbyteDeformatter; 

				HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter); 
				System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider(); 

				RSA.FromXmlString(p_strKeyPublic); 
				System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA); 
				//ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
				RSADeformatter.SetHashAlgorithm("MD5"); 

				DeformatterData =Convert.FromBase64String(p_strDeformatterData); 

				if(RSADeformatter.VerifySignature(HashbyteDeformatter,DeformatterData)) 
				{ 
					return true; 
				} 
				else 
				{ 
					return false; 
				} 
			
		} 


		#endregion 


		#endregion 

	} 
} 
