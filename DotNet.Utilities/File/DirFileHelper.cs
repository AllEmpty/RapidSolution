/// <summary>
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Text;
using System.IO;
using System.Web;

namespace DotNet.Utilities
{
    /// <summary>
    /// 文件操作夹
    /// </summary>
    public static class DirFileHelper
    {
        #region 检测指定目录是否存在
        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        /// <returns></returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        #endregion

        #region 检测指定文件是否存在,如果存在返回true
        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static bool IsExistFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            if (filePath.IndexOf(":", System.StringComparison.Ordinal) < 0) { filePath = GetMapPath(filePath); }

            return File.Exists(filePath);
        }

        #endregion

        #region 获取指定目录中的文件列表
        /// <summary>
        /// 获取指定目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            //获取文件列表
            return Directory.GetFiles(directoryPath);
        }
        #endregion

        #region 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
        /// <summary>
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static string[] GetDirectories(string directoryPath)
        {
            try
            {
                return Directory.GetDirectories(directoryPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取指定目录及子目录中所有文件列表
        /// <summary>
        /// 获取指定目录及子目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 检测指定目录是否为空
        /// <summary>
        /// 检测指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                //判断是否存在文件
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    return false;
                }

                //判断是否存在文件夹
                string[] directoryNames = GetDirectories(directoryPath);
                if (directoryNames.Length > 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                //这里记录日志
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return true;
            }
        }
        #endregion

        #region 检测指定目录中是否存在指定的文件
        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>        
        public static bool Contains(string directoryPath, string searchPattern)
        {
            try
            {
                //获取指定的文件列表
                string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //获取指定的文件列表
                string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }
        #endregion

        #region 创建目录
        ///// <summary>
        ///// 创建目录
        ///// </summary>
        ///// <param name="dir">要创建的目录路径包括目录名</param>
        //public static void CreateDir(string dir)
        //{
        //    if (dir.Length == 0) return;
        //    if (!Directory.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
        //        Directory.CreateDirectory(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
        //}

        /// <summary>创建目录</summary>
        /// <param name="dirpath">路径</param>
        /// <returns></returns>
        public static bool CreateDir(string dirpath)
        {
            if (string.IsNullOrEmpty(dirpath)) return false;

            CheckSaveDir(dirpath);
            return DirExists(dirpath);
        }
        #endregion

        #region 删除目录
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">要删除的目录路径和名称</param>
        public static void DeleteDir(string dir)
        {
            if (dir.Length == 0) return;
            if (Directory.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
                Directory.Delete(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file">要删除的文件路径和名称</param>
        public static bool DeleteFile(string file)
        {
            if (string.IsNullOrEmpty(file)) return false;
            if (file.IndexOf(":") < 0) { file = GetMapPath(file); }

            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                    return (!File.Exists(file));
                }
                catch
                {
                    return false;
                }
            }
            return false;

            //if (File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + file))
            //    File.Delete(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + file);
        }
        #endregion

        #region 创建文件
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="dir">带后缀的文件名</param>
        /// <param name="pagestr">文件内容</param>
        public static void CreateFile(string dir, string pagestr)
        {
            dir = dir.Replace("/", "\\");
            if (dir.IndexOf("\\") > -1)
                CreateDir(dir.Substring(0, dir.LastIndexOf("\\")));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir, false, System.Text.Encoding.GetEncoding("GB2312"));
            sw.Write(pagestr);
            sw.Close();
        }
        #endregion

        #region 移动文件(剪贴--粘贴)
        /// <summary>
        /// 移动文件(剪贴--粘贴)
        /// </summary>
        /// <param name="dir1">要移动的文件的路径及全名(包括后缀)</param>
        /// <param name="dir2">文件移动到新的位置,并指定新的文件名</param>
        public static void MoveFile(string dir1, string dir2)
        {
            dir1 = dir1.Replace("/", "\\");
            dir2 = dir2.Replace("/", "\\");
            if (File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1))
                File.Move(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir2);
        }
        #endregion

        #region 复制文件
        ///// <summary>
        ///// 复制文件
        ///// </summary>
        ///// <param name="dir1">要复制的文件的路径已经全名(包括后缀)</param>
        ///// <param name="dir2">目标位置,并指定新的文件名</param>
        //public static void CopyFile(string dir1, string dir2)
        //{
        //    dir1 = dir1.Replace("/", "\\");
        //    dir2 = dir2.Replace("/", "\\");
        //    if (File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1))
        //    {
        //        File.Copy(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir2, true);
        //    }
        //}

        /// <summary>复制文件</summary>
        /// <param name="oldFile">原始文件名(包括完整路径)</param>
        /// <param name="newFile">目标文件名(包括完整路径)</param>
        /// <returns></returns>
        public static bool CopyFile(string oldFile, string newFile)
        {
            if (string.IsNullOrEmpty(oldFile)) return false;
            if (oldFile.IndexOf(":") < 0) { oldFile = GetMapPath(oldFile); }
            if (newFile.IndexOf(":") < 0) { newFile = GetMapPath(newFile); }

            if (File.Exists(oldFile))
            {
                try
                {
                    File.Copy(oldFile, newFile, true);
                    return (File.Exists(newFile));
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region 根据时间得到目录名 / 格式:yyyyMMdd 或者 HHmmssff
        /// <summary>
        /// 根据时间得到目录名yyyyMMdd
        /// </summary>
        /// <returns></returns>
        public static string GetDateDir()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        /// <summary>
        /// 根据时间得到文件名HHmmssff
        /// </summary>
        /// <returns></returns>
        public static string GetDateFile()
        {
            return DateTime.Now.ToString("HHmmssff");
        }
        #endregion

        #region 复制文件夹
        /// <summary>
        /// 复制文件夹(递归)
        /// </summary>
        /// <param name="varFromDirectory">源文件夹路径</param>
        /// <param name="varToDirectory">目标文件夹路径</param>
        public static void CopyFolder(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyFolder(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(varFromDirectory);
            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, varToDirectory + s.Substring(s.LastIndexOf("\\")), true);
                }
            }
        }
        #endregion

        #region 检查文件,如果文件不存在则创建
        /// <summary>
        /// 检查文件,如果文件不存在则创建  
        /// </summary>
        /// <param name="FilePath">路径,包括文件名</param>
        public static void ExistsFile(string FilePath)
        {
            //if(!File.Exists(FilePath))    
            //File.Create(FilePath);    
            //以上写法会报错,详细解释请看下文.........   
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
        }
        #endregion

        #region 检查文件是否存在

        /// <summary>返回目录是否存在</summary>
        /// <param name="dirname">目录名</param>
        /// <returns>是否存在</returns>
        public static bool DirExists(string dirname)
        {
            if (string.IsNullOrEmpty(dirname)) return false;

            return Directory.Exists(dirname);
        }
        #endregion

        #region 删除指定文件夹对应其他文件夹里的文件
        /// <summary>
        /// 删除指定文件夹对应其他文件夹里的文件
        /// </summary>
        /// <param name="varFromDirectory">指定文件夹路径</param>
        /// <param name="varToDirectory">对应其他文件夹路径</param>
        public static void DeleteFolderFiles(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    DeleteFolderFiles(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }


            string[] files = Directory.GetFiles(varFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Delete(varToDirectory + s.Substring(s.LastIndexOf("\\")));
                }
            }
        }
        #endregion

        #region 从文件的绝对路径中获取文件名( 包含扩展名 )
        ///// <summary>
        ///// 从文件的绝对路径中获取文件名( 包含扩展名 )
        ///// </summary>
        ///// <param name="filePath">文件的绝对路径</param>        
        //public static string GetFileName(string filePath)
        //{
        //    //获取文件的名称
        //    FileInfo fi = new FileInfo(filePath);
        //    return fi.Name;
        //}

        /// <summary>从路径中抽取文件名(包括扩展名)</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFileName(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            if (str.Length > 0)
            {
                if (str.LastIndexOf("/") > 0)
                {
                    str = str.Substring(str.LastIndexOf("/") + 1);
                }
                else if (str.LastIndexOf("\\") > 0)
                {
                    str = str.Substring(str.LastIndexOf("\\") + 1);
                }
            }
            return str;
        }

        /// <summary>从路径中抽取文件名(是否包含扩展名)</summary>
        /// <param name="str"></param>
        /// <param name="noExt">=true时(不包括扩展名)</param>
        /// <returns></returns>
        public static string GetFileName(string str, bool noExt = false)
        {
            if (string.IsNullOrEmpty(str)) return "";

            if (str.Length > 0)
            {
                str = GetFileName(str);
                if (noExt & str.LastIndexOf(".") > 0)
                {
                    str = str.Substring(0, str.LastIndexOf("."));
                }
            }
            return str;
        }
        #endregion

        
        #region 创建一个目录
        /// <summary>
        /// 创建一个目录
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        #endregion

        #region 创建一个文件
        /// <summary>
        /// 创建一个文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 创建一个文件,并将字节流写入文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //写入二进制流
                    fs.Write(buffer, 0, buffer.Length);

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }
        #endregion

        #region 获取文本文件的行数
        /// <summary>
        /// 获取文本文件的行数
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static int GetLineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中
            string[] rows = File.ReadAllLines(filePath);

            //返回行数
            return rows.Length;
        }
        #endregion

        #region 获取一个文件的长度
        /// <summary>
        /// 获取一个文件的长度,单位为Byte
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static int GetFileSize(string filePath)
        {
            //创建一个文件对象
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小
            return (int)fi.Length;
        }
        #endregion

        #region 获取指定目录中的子目录列表
        /// <summary>
        /// 获取指定目录及子目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 向文本文件写入内容

        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>
        /// <param name="encoding">编码</param>
        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            //向文件写入内容
            File.WriteAllText(filePath, text, encoding);
        }
        #endregion

        #region 向文本文件的尾部追加内容
        /// <summary>
        /// 向文本文件的尾部追加内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">写入的内容</param>
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }
        #endregion

        #region 将现有文件的内容复制到新文件中
        /// <summary>
        /// 将源文件的内容复制到目标文件中
        /// </summary>
        /// <param name="sourceFilePath">源文件的绝对路径</param>
        /// <param name="destFilePath">目标文件的绝对路径</param>
        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }
        #endregion

        #region 将文件移动到指定目录
        /// <summary>
        /// 将文件移动到指定目录
        /// </summary>
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            //获取源文件的名称
            string sourceFileName = GetFileName(sourceFilePath);

            if (IsExistDirectory(descDirectoryPath))
            {
                //如果目标中存在同名文件,则删除
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                {
                    DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                }
                //将文件移动到指定目录
                File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }
        #endregion

        #region 从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// <summary>
        /// 从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetFileNameNoExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Name.Split('.')[0];
        }
        #endregion

        #region 从文件的绝对路径中获取扩展名
        /// <summary>
        /// 从文件的绝对路径中获取扩展名
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Extension;
        }

        /// <summary>从文件名中抽取扩展名</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFileExtension(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            if (str.Length > 0)
            {
                if (str.LastIndexOf(".") > 0)
                {
                    str = str.Substring(str.LastIndexOf(".") + 1);
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }
        #endregion

        #region 清空指定目录
        /// <summary>
        /// 清空指定目录下所有文件及子目录,但该目录依然保存.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void ClearDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                //删除目录中所有的文件
                string[] fileNames = GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    DeleteFile(fileNames[i]);
                }

                //删除目录中所有的子目录
                string[] directoryNames = GetDirectories(directoryPath);
                for (int i = 0; i < directoryNames.Length; i++)
                {
                    DeleteDirectory(directoryNames[i]);
                }
            }
        }
        #endregion

        #region 清空文件内容
        /// <summary>
        /// 清空文件内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void ClearFile(string filePath)
        {
            //删除文件
            File.Delete(filePath);

            //重新创建该文件
            CreateFile(filePath);
        }
        #endregion

        #region 删除指定目录
        /// <summary>
        /// 删除指定目录及其所有子目录
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void DeleteDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
        #endregion

        #region 修正路径右边缺少"/"
        /// <summary>修正路径右边缺少"/"</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FixDirPath(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            if (str.Length > 0)
            {
                str = str.Replace("\\", "/");

                if (!str.EndsWith("/")) { str += "/"; }
            }
            return str;
        }
        #endregion

        #region 创建目录,如果父目录不存在,将一级级生成
        /// <summary>创建目录,如果父目录不存在,将一级级生成.</summary>
        /// <param name="sCheckPath">/newsfile/2009/07/</param>
        /// <returns>返回创建目录是否成功</returns>
        public static bool CheckSaveDir(string sCheckPath)
        {
            if (sCheckPath == "")
            {
                return false;
            }

            string fPath = sCheckPath;
            if (!fPath.EndsWith("/"))
            {
                fPath += "/";
            }

            fPath = GetMapPath(fPath);
            if (Directory.Exists(fPath))
            {
                return true;
            }
            else
            {
                int iRootCount = GetMapPath("\\").Split('\\').Length;
                int iDirCount = fPath.Split('\\').Length;

                string[] aPathRs = fPath.Split('\\');
                string tPath = aPathRs[0] + "\\";

                for (int i = 1; i < iDirCount; i++)
                {
                    tPath += aPathRs[i] + "\\";
                    if (i >= iRootCount && i <= iDirCount)
                    {
                        try
                        {
                            if (!Directory.Exists(tPath)) Directory.CreateDirectory(tPath);
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
                return Directory.Exists(fPath);
            }
        }
        #endregion

        #region 获得当前绝对路径
        /// <summary>获得当前绝对路径</summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Server.MapPath(strPath);
                }
                else
                {
                    strPath = strPath.Replace("/", "\\");
                    if (strPath.StartsWith("\\"))
                    {
                        strPath = strPath.TrimStart('\\');
                    }
                    return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 从文件名中加后缀字符,组成新文件名,用于缩略图
        /// <summary>从文件名中加后缀字符,组成新文件名,用于缩略图<para />
        /// 例如:getFileNamePostfix("090801.gif","s") return "090801s.gif" <para />
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="sPostfix">后缀字符</param>
        /// <returns></returns>
        public static string GetFileNamePostfix(string sFileName, string sPostfix)
        {
            if (string.IsNullOrEmpty(sFileName)) return "";

            //如果是路径,则抽取文件名
            string str = GetFileName(sFileName);

            if (str.Length > 0)
            {
                if (str.LastIndexOf(".") > 0)
                {
                    int iTmp = str.LastIndexOf(".");

                    str = str.Substring(0, iTmp) + sPostfix + str.Substring(iTmp);
                }
                else
                {
                    str += sPostfix;
                }
            }
            return str;
        }

        /// <summary>从文件名中加后缀字符,组成新文件名,用于缩略图,(包括路径)<para />
        /// 例如:getFilePathPostfix("090801.gif","s") return "090801s.gif" <para />
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="sPostfix">后缀字符</param>
        /// <returns></returns>
        public static string GetFilePathPostfix(string sFileName, string sPostfix)
        {
            if (string.IsNullOrEmpty(sFileName)) return "";

            string str = sFileName;

            if (str.Length > 0)
            {
                if (str.LastIndexOf(".") > 0)
                {
                    int iTmp = str.LastIndexOf(".");

                    str = str.Substring(0, iTmp) + sPostfix + str.Substring(iTmp);
                }
                else
                {
                    str += sPostfix;
                }
            }
            return str;
        }
        #endregion

        #region 删除图片文件,连同相关的大型图,中型图,微型图一并删除
        /// <summary>删除图片文件,连同相关的大型图,中型图,微型图一并删除</summary>
        /// <param name="filename">文件名(包括完整路径)</param>
        /// <returns></returns>
        public static bool DelPicFile(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return false;

            string bigImg = GetFilePathPostfix(filename, "b");
            string midImg = GetFilePathPostfix(filename, "m");
            string minImg = GetFilePathPostfix(filename, "s");
            string orgImg = GetFilePathPostfix(filename, "o");
            string hotImg = GetFilePathPostfix(filename, "h");

            DeleteFile(filename);
            DeleteFile(bigImg);
            DeleteFile(midImg);
            DeleteFile(minImg);
            DeleteFile(orgImg);
            DeleteFile(hotImg);

            return true;
        }
        #endregion

        #region 返回文件文件大小（Size）的字符格式
        /// <summary>返回文件Size的字符格式</summary>
        /// <param name="bytes">bytes</param>
        /// <returns>例如:1024=1Kb</returns>
        public static string FmtFileSize(long bytes)
        {
            if (bytes >= 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "GB";
            }
            if (bytes >= 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "MB";
            }
            if (bytes >= 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "KB";
            }
            return bytes.ToString() + "bytes";
        }

        /// <summary>返回文件Size的字符格式（注意：传入参数为kb）</summary>
        /// <param name="kb">kb</param>
        /// <returns>例如:1024=1Kb</returns>
        public static string FmtFileSize2(int kb)
        {
            if (kb >= 1048576)
            {
                return ((double)(kb / 1048576)).ToString("0") + "G";
            }
            if (kb >= 1024)
            {
                return ((double)(kb / 1024)).ToString("0") + "M";
            }
            return kb.ToString() + "K";
        }
        #endregion

        #region 从(路径+文件名)中抽取路径
        /// <summary>从(路径+文件名)中抽取路径</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFilePath(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            if (str.Length > 0)
            {
                if (str.LastIndexOf("/") > 0)
                {
                    str = str.Substring(0, str.LastIndexOf("/"));
                }
                else if (str.LastIndexOf("\\") > 0)
                {
                    str = str.Substring(0, str.LastIndexOf("\\"));
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }
        #endregion

        #region 取得随机文件名(原文件名),用yyMMddhhmmss + (xxx),共15位数字
        /// <summary> 取得随机文件名(原文件名),用yyMMddhhmmss + (xxx),共15位数字</summary>
        /// <param name="fileName">原文件名或文件扩展名</param>
        /// <returns></returns>
        public static string GetRndFileName(string fileName)
        {
            string sDate = RandomHelper.GetDateRnd();
            string fileExt = "";

            if (fileName.LastIndexOf(".") > 0)
            {
                fileExt = fileName.Substring(fileName.LastIndexOf("."));
            }
            else
            {
                fileExt = fileName;
            }
            return sDate + fileExt;
        }
        #endregion
    }
}
