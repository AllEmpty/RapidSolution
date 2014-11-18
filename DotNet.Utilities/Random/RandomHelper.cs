/// <summary>
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// 使用Random类生成伪随机数
    /// </summary>
    public class RandomHelper
    {
        #region 生成一个指定范围的随机整数
        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public static int GetRandomInt(int minNum, int maxNum)
        {
            var random = new Random();
            return random.Next(minNum, maxNum);
        }
        #endregion

        #region 生成一个0.0到1.0的随机小数
        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public static double GetRandomDouble()
        {
            var random = new Random();
            return random.NextDouble();
        }
        #endregion

        #region 对一个数组进行随机排序
        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public static void GetRandomArray<T>(T[] arr)
        {
            //对数组进行随机排序的算法:随机选择两个位置，将两个位置上的值交换

            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;

            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成两个随机数位置
                int randomNum1 = GetRandomInt(0, arr.Length);
                int randomNum2 = GetRandomInt(0, arr.Length);

                //定义临时变量
                T temp;

                //交换两个随机数位置的值
                temp = arr[randomNum1];
                arr[randomNum1] = arr[randomNum2];
                arr[randomNum2] = temp;
            }
        }


        // 一：随机生成不重复数字字符串 
        private static int rep = 0;
        public static string GenerateCheckCodeNum(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return str;
        }

        //方法二：随机生成字符串（数字和字母混和）
        public static string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        #endregion

        #region 从字符串里随机得到，规定个数的字符串.
        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
        /// </summary>
        /// <param name="allChar">字符规范，如果等于null时，默认值为："1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"</param>
        /// <param name="codeCount">需要生成的随机数个数</param>
        /// <returns></returns>
        public static string GetRandomCode(string allChar, int codeCount)
        {
            if (string.IsNullOrEmpty(allChar))
            {
                allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            }
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            var rand = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        #endregion


        #region 随机数操作函数
        /// <summary>取得随机数(数字),用yyMMddhhmmss + (xxx),共15位数字</summary>
        /// <returns></returns>
        public static string GetDateRnd()
        {
            DateTime dtTmp = DateTime.Now;
            return dtTmp.ToString("yyMMddhhmmss") + GetRndNum(3);
        }

        /// <summary> 取得随机数(字母+数字),用yyMMddhhmmss + (xxx),共15位字母或数字,</summary>
        /// <returns></returns>
        public static string GetRndKey()
        {
            DateTime dtTmp = DateTime.Now;
            return dtTmp.ToString("yyMMddhhmmss") + GetRndNum(3, true);
        }

        /// <summary> 取得n位随机整数,:45546</summary>
        /// <param name="n">随机数长度</param>
        /// <param name="isStr">true=随机字母和整数，false=随机整数</param>
        /// <returns></returns>
        public static string GetRndNum(int n, bool isStr = false)
        {
            string cChar = "0123456789";
            if (isStr)
            {
                cChar = "abcdefghijklmnopqrstuvwxyz0123456789";
            }

            int cLen = cChar.Length;
            string sRet = "";
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < n; i++)
            {
                sRet += cChar[rnd.Next(0, cLen)].ToString();
            }
            return sRet;
        }

        /// <summary>取得区间中的随机数,例如:getRndNext(14,17),将返回14,15,16</summary>
        /// <param name="min">随机数的最小值</param> 
        /// <param name="max">随机数的最大值(结果小于该值)</param> 
        /// <returns></returns>
        public static int GetRndNext(int min, int max)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            int t = 0;
            if (min > max)
            {
                t = min;
                min = max;
                max = t;
            }

            t = max - min;
            return rnd.Next(t) + min;
        }

        /// <summary>取得区间中的随机数,例如:getRndNext(14,17),将返回14,15,16</summary>
        /// <param name="min">随机数的最小值</param> 
        /// <param name="max">随机数的最大值(结果小于该值)</param> 
        /// <returns></returns>
        public static decimal GetRndNextDecimal(decimal min, decimal max)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            decimal t = 0;
            if (min > max)
            {
                t = min;
                min = max;
                max = t;
            }

            t = max - min;
            return (decimal)rnd.NextDouble() * t + min;
        }
        #endregion
    }
}
