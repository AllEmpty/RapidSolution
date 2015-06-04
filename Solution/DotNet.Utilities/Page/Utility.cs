/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>

using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace DotNet.Utilities
{
	/// <summary>
	/// Utility ��ժҪ˵����
	/// </summary>
	public class Utility:Page
	{

		#region ����ת��
		/// <summary>
		/// ���ض���obj��Stringֵ,objΪnullʱ���ؿ�ֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>�ַ�����</returns>
		public static string ToObjectString(object obj)  
		{
			return null == obj ? String.Empty : obj.ToString();
		}

		/// <summary>
		/// ������ת��Ϊ��ֵ(Int32)����,ת��ʧ�ܷ���-1��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Int32��ֵ��</returns>
		public static int ToInt(object obj)
		{
			try
			{
				return int.Parse(ToObjectString(obj));
			}
			catch
			{ return -1; }
		}

		/// <summary>
		/// ������ת��Ϊ��ֵ(Int32)���͡�
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>Int32��ֵ��</returns>
		public static int ToInt(object obj,int returnValue)
		{
			try
			{
				return int.Parse(ToObjectString(obj));
			}
			catch
			{ return returnValue; }
		}
		/// <summary>
		/// ������ת��Ϊ��ֵ(Long)����,ת��ʧ�ܷ���-1��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Long��ֵ��</returns>
		public static long ToLong(object obj)
		{
			try
			{
				return long.Parse(ToObjectString(obj));
			}
			catch
			{ return -1L; }
		}
		/// <summary>
		/// ������ת��Ϊ��ֵ(Long)���͡�
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>Long��ֵ��</returns>
		public static long ToLong(object obj,long returnValue)
		{
			try
			{
				return long.Parse(ToObjectString(obj));
			}
			catch
			{ return returnValue; }
		}
		/// <summary>
		/// ������ת��Ϊ��ֵ(Decimal)����,ת��ʧ�ܷ���-1��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Decimal��ֵ��</returns>
		public static decimal ToDecimal(object obj)
		{
			try
			{
				return decimal.Parse(ToObjectString(obj));
			}
			catch
			{ return -1M; }
		}

		/// <summary>
		/// ������ת��Ϊ��ֵ(Decimal)���͡�
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>Decimal��ֵ��</returns>
		public static decimal ToDecimal(object obj,decimal returnValue)
		{
			try
			{
				return decimal.Parse(ToObjectString(obj));
			}
			catch
			{ return returnValue; }
		}
		/// <summary>
		/// ������ת��Ϊ��ֵ(Double)����,ת��ʧ�ܷ���-1��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Double��ֵ��</returns>
		public static double ToDouble(object obj)
		{
			try
			{
				return double.Parse(ToObjectString(obj));
			}
			catch
			{ return -1; }
		}

		/// <summary>
		/// ������ת��Ϊ��ֵ(Double)���͡�
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>Double��ֵ��</returns>
		public static double ToDouble(object obj,double returnValue)
		{
			try
			{
				return double.Parse(ToObjectString(obj));
			}
			catch
			{ return returnValue; }
		}
		/// <summary>
		/// ������ת��Ϊ��ֵ(Float)����,ת��ʧ�ܷ���-1��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Float��ֵ��</returns>
		public static float ToFloat(object obj)
		{
			try
			{
				return float.Parse(ToObjectString(obj));
			}
			catch
			{ return -1; }
		}

		/// <summary>
		/// ������ת��Ϊ��ֵ(Float)���͡�
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>Float��ֵ��</returns>
		public static float ToFloat(object obj,float returnValue)
		{
			try
			{
				return float.Parse(ToObjectString(obj));
			}
			catch
			{ return returnValue; }
		}
		/// <summary>
		/// ������ת��Ϊ��ֵ(DateTime)����,ת��ʧ�ܷ���Now��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>DateTimeֵ��</returns>
		public static DateTime ToDateTime(object obj)
		{
			try
			{
				DateTime dt = DateTime.Parse(ToObjectString(obj));
				if( dt > DateTime.MinValue && DateTime.MaxValue > dt)
					return dt;
				return DateTime.Now;
			}
			catch
			{ return DateTime.Now; }
		}

		/// <summary>
		/// ������ת��Ϊ��ֵ(DateTime)���͡�
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>DateTimeֵ��</returns>
		public static DateTime ToDateTime(object obj,DateTime returnValue)
		{
			try
			{
				DateTime dt = DateTime.Parse(ToObjectString(obj));
				if( dt > DateTime.MinValue && DateTime.MaxValue > dt)
					return dt;
				return returnValue;
			}
			catch
			{ return returnValue; }
		}
		/// <summary>
		/// ��Booleanת����byte,ת��ʧ�ܷ���0��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Byteֵ��</returns>
		public static byte ToByteByBool( object obj )
		{
			string text = ToObjectString(obj).Trim();
			if( text == string.Empty)
				return 0;
			else
			{
				try
				{
					return (byte)(text.ToLower()=="true"?1:0);
				}
				catch
				{
					return 0;
				}
			}
		}
		
		/// <summary>
		/// ��Booleanת����byte��
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>Byteֵ��</returns>
		public static byte ToByteByBool( object obj, byte returnValue )
		{
			string text = ToObjectString(obj).Trim();
			if( text == string.Empty)
				return returnValue;
			else
			{
				try
				{
					return (byte)(text.ToLower()=="true"?1:0);
				}
				catch
				{
					return returnValue;
				}
			}
		}
		/// <summary>
		/// ��byteת����Boolean,ת��ʧ�ܷ���false��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Booleanֵ��</returns>
		public static bool ToBoolByByte( object obj )
		{
			try
			{
				string s = ToObjectString(obj).ToLower();
				return s == "1" || s== "true"?true:false;
			}
			catch
			{
				return false;
			}
		}
		/// <summary>
		/// ��byteת����Boolean��
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="returnValue">ת��ʧ�ܷ��ظ�ֵ��</param>
		/// <returns>Booleanֵ��</returns>
		public static bool ToBoolByByte( object obj, bool returnValue )
		{
			try
			{
				string s = ToObjectString(obj).ToLower();
				return s == "1" || s== "true"?true:false;
			}
			catch
			{
				return returnValue;
			}
		}
		#endregion

		#region �����ж�
		/// <summary>
		/// �ж��ı�obj�Ƿ�Ϊ��ֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Booleanֵ��</returns>
		public static bool IsEmpty(string obj)
		{
			return ToObjectString(obj).Trim() == String.Empty ? true : false;
		}

		/// <summary>
		/// �ж϶����Ƿ�Ϊ��ȷ������ֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Boolean��</returns>
		public static bool IsDateTime(object obj)
		{
			try
			{
				DateTime dt = DateTime.Parse(ToObjectString(obj));
				if( dt > DateTime.MinValue && DateTime.MaxValue > dt)
					return true;
				return false;
			}
			catch
			{ return false; }
		}

		/// <summary>
		/// �ж϶����Ƿ�Ϊ��ȷ��Int32ֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Int32ֵ��</returns>
		public static bool IsInt(object obj)
		{
			try
			{
				int.Parse(ToObjectString(obj));
				return true;
			}
			catch
			{ return false; }
		}

		/// <summary>
		/// �ж϶����Ƿ�Ϊ��ȷ��Longֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Longֵ��</returns>
		public static bool IsLong(object obj)
		{
			try
			{
				long.Parse(ToObjectString(obj));
				return true;
			}
			catch
			{ return false; }
		}

		/// <summary>
		/// �ж϶����Ƿ�Ϊ��ȷ��Floatֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Floatֵ��</returns>
		public static bool IsFloat(object obj)
		{
			try
			{
				float.Parse(ToObjectString(obj));
				return true;
			}
			catch
			{ return false; }
		}

		/// <summary>
		/// �ж϶����Ƿ�Ϊ��ȷ��Doubleֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Doubleֵ��</returns>
		public static bool IsDouble(object obj)
		{
			try
			{
				double.Parse(ToObjectString(obj));
				return true;
			}
			catch
			{ return false; }
		}
		
		/// <summary>
		/// �ж϶����Ƿ�Ϊ��ȷ��Decimalֵ��
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>Decimalֵ��</returns>
		public static bool IsDecimal(object obj)
		{
			try
			{
				decimal.Parse(ToObjectString(obj));
				return true;
			}
			catch
			{ return false; }
		}
		#endregion

		#region ���ݲ���
		/// <summary>
		/// ȥ���ַ��������пո�
		/// </summary>
		/// <param name="text">�ַ���</param>
		/// <returns>�ַ���</returns>
		public static string StringTrimAll( string text )
		{
			string _text = ToObjectString(text);
			string returnText = String.Empty;
			char[] chars = _text.ToCharArray();
			for( int i=0;i<chars.Length;i++)
			{
				if( chars[i].ToString() != string.Empty)
					returnText += chars[i].ToString();
			}
			return returnText;
		}

		/// <summary>
		/// ȥ����ֵ�ַ��������пո�
		/// </summary>
		/// <param name="numricString">��ֵ�ַ���</param>
		/// <returns>String</returns>
		public static string NumricTrimAll( string numricString )
		{
			string text = ToObjectString(numricString).Trim();
			string returnText = String.Empty;
			char[] chars = text.ToCharArray();
			for( int i=0;i<chars.Length;i++)
			{
				if( chars[i].ToString() == "+" || chars[i].ToString() == "-" || IsDouble( chars[i].ToString()) )
					returnText += chars[i].ToString();
			}
			return returnText;
		}

		/// <summary>
		/// �������в���ƥ���������
		/// </summary>
		/// <param name="array">����</param>
		/// <param name="obj">����</param>
		/// <returns>Boolean</returns>
		public static bool ArrayFind(Array array,object obj)
		{
			bool b = false;
			foreach(object obj1 in array)
			{
				if(obj.Equals(obj1))
				{
					b = true;
					break;
				}
			}
			return b;
		}

		/// <summary>
		/// �������в���ƥ���ַ���
		/// </summary>
		/// <param name="array">����</param>
		/// <param name="obj">����</param>
		/// <param name="unUpLower">�Ƿ���Դ�Сд</param>
		/// <returns>Boolean</returns>
		public static bool ArrayFind(Array array,string obj,bool unUpLower)
		{
			bool b = false;
			foreach(string obj1 in array)
			{
				if(!unUpLower)
				{
					if(obj.Trim().Equals(obj1.ToString().Trim()))
					{
						b = true;
						break;
					}
				}
				else
				{
					if(obj.Trim().ToUpper().Equals(obj1.ToString().Trim().ToUpper()))
					{
						b = true;
						break;
					}
				}
			}
			return b;
		}
		/// <summary>
		/// �滻�ַ����еĵ����š�
		/// </summary>
		/// <param name="inputString">�ַ���</param>
		/// <returns>String</returns>
		public static string ReplaceInvertedComma( string inputString )
		{
			return inputString.Replace("'","''");
		}

		
		/// <summary>
		/// �ж������ֽ������Ƿ������ֵͬ.
		/// </summary>
		/// <param name="bytea">�ֽ�1</param>
		/// <param name="byteb">�ֽ�2</param>
		/// <returns>Boolean</returns>
		public static bool CompareByteArray(byte[] bytea,byte[] byteb)
		{
			if(null == bytea || null == byteb)
			{
				return false;
			}
			else
			{
				int aLength = bytea.Length;
				int bLength = byteb.Length;
				if(aLength != bLength)
					return false;
				else
				{
					bool compare = true;
					for(int index = 0; index < aLength; index++)
					{
						if(bytea[index].CompareTo(byteb[index]) != 0)
						{
							compare = false;
							break;
						}
					}
					return compare;
				}
			}
		}

		
		/// <summary>
		/// �����������ɡ�
		/// </summary>
		/// <param name="inputText">�ַ���</param>
		/// <returns>DateTime</returns>
		public static string BuildDate( string inputText )
		{
			try
			{
				return DateTime.Parse( inputText ).ToShortDateString();
			}
			catch
			{
				string text = NumricTrimAll( inputText );
				string year = DateTime.Now.Year.ToString();
				string month = DateTime.Now.Month.ToString();
				string day = DateTime.Now.Day.ToString();
				int length = text.Length;
				if( length == 0 )
					return String.Empty;
				else
				{
					if( length<=2 )
						day = text;
					else if( length<=4 )
					{
						month = text.Substring(0,2);
						day = text.Substring(2,length-2);
					}
					else if( length<=6 )
					{
						year = text.Substring(0,4);
						month = text.Substring(4,length-4);
					}
					else if( length>6)
					{
						year = text.Substring(0,4);
						month = text.Substring(4,2);
						day = text.Substring(6,length-6);
					}
					try
					{
						return DateTime.Parse( year+"-"+month+"-"+day ).ToShortDateString();
					}
					catch
					{
						return String.Empty;
					}
				}
			}
		}

		

		/// <summary>
		/// ����ļ��Ƿ���ʵ���ڡ�
		/// </summary>
		/// <param name="path">�ļ�ȫ��������·������</param>
		/// <returns>Boolean</returns>
		public static bool IsFileExists(string path)
		{
			try
			{
				return File.Exists(path);
			}
			catch
			{	return false; }
		}

		/// <summary>
		/// ���Ŀ¼�Ƿ���ʵ���ڡ�
		/// </summary>
		/// <param name="path">Ŀ¼·��.</param>
		/// <returns>Boolean</returns>
		public static bool IsDirectoryExists(string path)
		{
			try
			{
				return Directory.Exists(Path.GetDirectoryName(path));
			}
			catch
			{	return false; }
		}
		
		/// <summary>
		/// �����ļ����Ƿ����ƥ���С�
		/// </summary>
		/// <param name="fi">Ŀ���ļ�.</param>
		/// <param name="lineText">Ҫ���ҵ����ı�.</param>
		/// <param name="lowerUpper">�Ƿ����ִ�Сд.</param>
		/// <returns>Boolean</returns>
		public static bool FindLineTextFromFile(FileInfo fi,string lineText,bool lowerUpper)
		{
			bool b = false;
			try
			{
				if(fi.Exists)
				{
					StreamReader sr=new StreamReader(fi.FullName);
					string g = "";
					do
					{
						g=sr.ReadLine();
						if(lowerUpper)
						{
							if(ToObjectString(g).Trim() == ToObjectString(lineText).Trim())
							{
								b = true;
								break;
							}
						}
						else
						{
							if(ToObjectString(g).Trim().ToLower() == ToObjectString(lineText).Trim().ToLower())
							{
								b = true;
								break;
							}
						}
					}
					while(sr.Peek()!=-1);
					sr.Close();
				}
			}
			catch
			{	b =false;	}
			return b;
		}


		/// <summary>
		/// �жϸ��Ӽ���ϵ�Ƿ���ȷ��
		/// </summary>
		/// <param name="table">���ݱ�</param>
		/// <param name="columnName">�Ӽ�������</param>
		/// <param name="parentColumnName">����������</param>
		/// <param name="inputString">�Ӽ�ֵ��</param>
		/// <param name="compareString">����ֵ��</param>
		/// <returns></returns>
		public static bool IsRightParent(DataTable table,string columnName,string parentColumnName,string inputString,string compareString)
		{
			ArrayList array = new ArrayList();
			SearchChild(array,table,columnName,parentColumnName,inputString,compareString);
			return array.Count == 0;
		}

		// �ڲ�����
		private static void SearchChild(ArrayList array,DataTable table,string columnName,string parentColumnName,string inputString,string compareString)
		{
			DataView view = new DataView(table);
			view.RowFilter = parentColumnName+"='"+ReplaceInvertedComma(inputString.Trim())+"'";//�ҳ����е����ࡣ
			//���ұ��е����ݵ�ID�Ƿ���compareString��ȣ���ȷ��� false;����ȼ���������
			for(int index = 0 ;index < view.Count;index ++)
			{
				if(Utility.ToObjectString(view[index][columnName]).ToLower() == compareString.Trim().ToLower())
				{
					array.Add("1");
					break;
				}
				else
				{
					SearchChild(array,table,columnName,parentColumnName,Utility.ToObjectString(view[index][columnName]),compareString);
				}
			}
		}

		#endregion

		#region ����

        /// <summary>
        /// ��ʽ���������ͣ������ַ���
        /// </summary>
        /// <param name="dtime">����</param>
        /// <param name="s">���������ռ������</param>
        /// <returns></returns>
		public static String Fomatdate(DateTime dtime,String s)
		{
			String datestr="";
			datestr=dtime.Year.ToString() + s + dtime.Month.ToString().PadLeft(2,'0')+ s +dtime.Day.ToString().PadLeft(2,'0');
			return datestr;
		}

        /// <summary>
        /// �������ڲ�
        /// </summary>
        /// <param name="sdmin">��ʼ����</param>
        /// <param name="sdmax">��������</param>
        /// <returns>���ڲ����Ϊʧ��</returns>
		public static int Datediff(DateTime sdmin,DateTime sdmax)
		{
			try
			{
				double i=0;
				while(sdmin.AddDays(i)<sdmax)
				{
					i++;
				}
				return Utility.ToInt(i);
			}
			catch
			{
			    return -1;
			}
		}

        /// <summary>
        /// �������ڲ�
        /// </summary>
        /// <param name="sdmin">��ʼ����</param>
        /// <param name="sdmax">��������</param>
        /// <returns>���ڲ����Ϊʧ��</returns>
		public static int Datediff(String sdmin,String sdmax)
		{
			try
			{
				DateTime dmin;
				DateTime dmax;
				dmin=DateTime.Parse(sdmin);
				dmax=DateTime.Parse(sdmax);
				double i=0;
				while(dmin.AddDays(i)<dmax)
				{
					i++;
				}
				return Utility.ToInt(i);
			}
			catch
			{
			    return -1;
			}
		}

		#endregion

		#region ת���û�����

		/// <summary>
		/// ���û�������ַ���ת��Ϊ�ɻ��С��滻Html���롢��Σ�����ݿ������ַ���ȥ����β�հס��İ�ȫ������롣
		/// </summary>
		/// <param name="inputString">�û������ַ���</param>
		public static string ConvertStr(string inputString)
		{
			string retVal=inputString;
			//retVal=retVal.Replace("&","&amp;"); 
			retVal=retVal.Replace("\"","&quot;"); 
			retVal=retVal.Replace("<","&lt;"); 
			retVal=retVal.Replace(">","&gt;"); 
			retVal=retVal.Replace(" ","&nbsp;"); 
			retVal=retVal.Replace("  ","&nbsp;&nbsp;"); 
			retVal=retVal.Replace("\t","&nbsp;&nbsp;");
			retVal=retVal.Replace("\r", "<br>");
			return retVal;
		}

		public static string InputText(string inputString)
		{
			string retVal=inputString;
			retVal= ConvertStr(retVal);
			retVal=retVal.Replace("[url]", "");
			retVal=retVal.Replace("[/url]", "");
			return retVal;
		}


        /// <summary>
        /// ��html������ʾ����ҳ��
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
		public static string OutputText(string inputString)
		{
			string retVal=System.Web.HttpUtility.HtmlDecode(inputString);
			retVal=retVal.Replace("<br>","");
			retVal=retVal.Replace("&amp","&;"); 
			retVal=retVal.Replace("&quot;","\""); 
			retVal=retVal.Replace("&lt;","<"); 
			retVal=retVal.Replace("&gt;",">"); 
			retVal=retVal.Replace("&nbsp;"," "); 
			retVal=retVal.Replace("&nbsp;&nbsp;","  "); 
			return retVal;
		}

		public static string ToUrl(string inputString)
		{
			string retVal=inputString;
			retVal= ConvertStr(retVal);
			return Regex.Replace(retVal,@"\[url](?<x>[^\]]*)\[/url]",@"<a href=""$1"" target=""_blank"">$1</a>",RegexOptions.IgnoreCase);
		}

		public static string GetSafeCode(string str)
		{
			  str=str.Replace("'","");
			  str=str.Replace(char.Parse("34"),' ');
			  str=str.Replace(";","");
			return str;
		}

		#endregion

   
        //#region ������

        ///// <summary>
        ///// �������˵���alert�Ի���
        ///// </summary>
        ///// <param name="str_Message">��ʾ��Ϣ,���ӣ�"������������!"</param>
        ///// <param name="page">Page��</param>
        //public static void Alert(string str_Message,Page page)
        //{
        //    page.RegisterStartupScript("","<script>alert('"+str_Message+"');</script>");
        //}
        ///// <summary>
        ///// �������˵���alert�Ի���
        ///// </summary>
        ///// <param name="str_Ctl_Name">��ý���ؼ�Idֵ,���磺txt_Name</param>
        ///// <param name="str_Message">��ʾ��Ϣ,���ӣ�"������������!"</param>
        ///// <param name="page">Page��</param>
        //public static void Alert(string str_Ctl_Name,string str_Message,Page page)
        //{
        //    page.RegisterStartupScript("","<script>alert('"+str_Message+"');document.forms(0)."+str_Ctl_Name+".focus(); document.forms(0)."+str_Ctl_Name+".select();</script>");
        //}
        ///// <summary>
        ///// �������˵���confirm�Ի���,�ú����и��׶�,����ŵ���Ӧ�¼������,Ŀǰû�����ƽ������
        ///// </summary>
        ///// <param name="str_Message">��ʾ��Ϣ,���ӣ�"���Ƿ�ȷ��ɾ��!"</param>
        ///// <param name="btn">����Botton��ťIdֵ,���磺btn_Flow</param>
        ///// <param name="page">Page��</param>
        //public static void Confirm(string str_Message,string btn,Page page)
        //{
        //    page.RegisterStartupScript("","<script> if (confirm('"+str_Message+"')==true){document.forms(0)."+btn+".click();}</script>");
        //}
        ///// <summary>
        /////  �������˵���confirm�Ի���,ѯ���û�׼��ת����Ӧ������������ȷ�����͡�ȡ����ʱ�Ĳ���
        ///// </summary>
        ///// <param name="str_Message">��ʾ��Ϣ�����磺"�ɹ���������,����\"ȷ��\"��ť��д����,����\"ȡ��\"�޸�����"</param>
        ///// <param name="btn_Redirect_Flow">"ȷ��"��ťidֵ</param>
        ///// <param name="btn_Redirect_Self">"ȡ��"��ťidֵ</param>
        ///// <param name="page">Page��</param>
        //public static void Confirm(string str_Message,string btn_Redirect_Flow,string btn_Redirect_Self,Page page)
        //{
        //    page.RegisterStartupScript("","<script> if (confirm('"+str_Message+"')==true){document.forms(0)."+btn_Redirect_Flow+".click();}else{document.forms(0)."+btn_Redirect_Self+".click();}</script>");
        //}

        //#endregion


		/// <summary>
		/// ���ð󶨵�DataGrid��DataTable�ļ�¼�������粻������ӿ���
		/// </summary>
		/// <param name="myDataTable">���ݱ�</param>
		/// <param name="intPageCount">DataGrid��ҳʱÿҳ����</param>
		public static void SetTableRows(DataTable myDataTable,int intPageCount)
		{
			try
			{
				int intTemp=myDataTable.Rows.Count%intPageCount;
				if ((myDataTable.Rows.Count==0) || (intTemp!=0))
				{
					for(int i=0;i<(intPageCount-intTemp);i++)
					{
						DataRow myDataRow=myDataTable.NewRow();
						myDataTable.Rows.Add(myDataRow);
					}
				}
			}
			catch
			{
				throw;
			}
		}


        public static string GetGuid(string guid)
        {
            return guid.Replace("-", "");
        }

        public static string ReadConfig(string filePath)
        {
            return System.Configuration.ConfigurationManager.AppSettings[filePath];
        }

        #region   �ַ�������������Ӣ�Ľ�ȡ
        /// <summary>   
        /// ��ȡ�ı���������Ӣ���ַ����������������ȣ�Ӣ����һ������
        /// </summary>
        /// <param name="str">����ȡ���ַ���</param>
        /// <param name="length">����㳤�ȵ��ַ���</param>
        /// <returns>string</returns>
        public static string GetSubString(string str, int length)
        {
            string temp = str;
            int j = 0;
            int k = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
                {
                    j += 2;
                }
                else
                {
                    j += 1;
                }
                if (j <= length)
                {
                    k += 1;
                }
                if (j > length)
                {
                    return temp.Substring(0, k) + "...";
                }
            }
            return temp;
        }
        #endregion
	}
}
