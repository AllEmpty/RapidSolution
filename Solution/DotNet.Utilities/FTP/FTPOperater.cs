/// <summary>
/// ��˵����CacheHelper
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Text;
using System.IO;

namespace DotNet.Utilities
{
    /// <summary>
    /// FTP������
    /// </summary>
    public class FTPOperater
    {
        #region ����
        private FTPClient ftp;
        /// <summary>
        /// ȫ��FTP���ʱ���
        /// </summary>
        public FTPClient Ftp
        {
            get { return ftp; }
            set { ftp = value; }
        }

        private string _server;
        /// <summary>
        /// Ftp������
        /// </summary>
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        private string _User;
        /// <summary>
        /// Ftp�û�
        /// </summary>
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }

        private string _Pass;
        /// <summary>
        /// Ftp����
        /// </summary>
        public string Pass
        {
            get { return _Pass; }
            set { _Pass = value; }
        }

        private string _FolderZJ;
        /// <summary>
        /// Ftp����
        /// </summary>
        public string FolderZJ
        {
            get { return _FolderZJ; }
            set { _FolderZJ = value; }
        }

        private string _FolderWX;
        /// <summary>
        /// Ftp����
        /// </summary>
        public string FolderWX
        {
            get { return _FolderWX; }
            set { _FolderWX = value; }
        }
        #endregion

        /// <summary>
        /// �õ��ļ��б�
        /// </summary>
        /// <returns></returns>
        public string[] GetList(string strPath)
        {
            if (ftp == null) ftp = this.getFtpClient();
            ftp.Connect();
            ftp.ChDir(strPath);
            return ftp.Dir("*");
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="ftpFolder">ftpĿ¼</param>
        /// <param name="ftpFileName">ftp�ļ���</param>
        /// <param name="localFolder">����Ŀ¼</param>
        /// <param name="localFileName">�����ļ���</param>
        public bool GetFile(string ftpFolder, string ftpFileName, string localFolder, string localFileName)
        {
            try
            {
                if (ftp == null) ftp = this.getFtpClient();
                if (!ftp.Connected)
                {
                    ftp.Connect();
                    ftp.ChDir(ftpFolder);
                }
                ftp.Get(ftpFileName, localFolder, localFileName);

                return true;
            }
            catch
            {
                try
                {
                    ftp.DisConnect();
                    ftp = null;
                }
                catch { ftp = null; }
                return false;
            }
        }

        /// <summary>
        /// �޸��ļ�
        /// </summary>
        /// <param name="ftpFolder">����Ŀ¼</param>
        /// <param name="ftpFileName">�����ļ���temp</param>
        /// <param name="localFolder">����Ŀ¼</param>
        /// <param name="localFileName">�����ļ���</param>
        public bool AddMSCFile(string ftpFolder, string ftpFileName, string localFolder, string localFileName, string BscInfo)
        {
            string sLine = "";
            string sResult = "";
            string path = "���Ӧ�ó������ڵ�������·��";
            path = path.Substring(0, path.LastIndexOf("\\"));
            try
            {
                FileStream fsFile = new FileStream(ftpFolder + "\\" + ftpFileName, FileMode.Open);
                FileStream fsFileWrite = new FileStream(localFolder + "\\" + localFileName, FileMode.Create);
                StreamReader sr = new StreamReader(fsFile);
                StreamWriter sw = new StreamWriter(fsFileWrite);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                while (sr.Peek() > -1)
                {
                    sLine = sr.ReadToEnd();
                }
                string[] arStr = sLine.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arStr.Length - 1; i++)
                {
                    sResult += BscInfo + "," + arStr[i].Trim() + "\n";
                }
                sr.Close();
                byte[] connect = new UTF8Encoding(true).GetBytes(sResult);
                fsFileWrite.Write(connect, 0, connect.Length);
                fsFileWrite.Flush();
                sw.Close();
                fsFile.Close();
                fsFileWrite.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="ftpFolder">ftpĿ¼</param>
        /// <param name="ftpFileName">ftp�ļ���</param>
        public bool DelFile(string ftpFolder, string ftpFileName)
        {
            try
            {
                if (ftp == null) ftp = this.getFtpClient();
                if (!ftp.Connected)
                {
                    ftp.Connect();
                    ftp.ChDir(ftpFolder);
                }
                ftp.Delete(ftpFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="ftpFolder">ftpĿ¼</param>
        /// <param name="ftpFileName">ftp�ļ���</param>
        public bool PutFile(string ftpFolder, string ftpFileName)
        {
            try
            {
                if (ftp == null) ftp = this.getFtpClient();
                if (!ftp.Connected)
                {
                    ftp.Connect();
                    ftp.ChDir(ftpFolder);
                }
                ftp.Put(ftpFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="ftpFolder">ftpĿ¼</param>
        /// <param name="ftpFileName">ftp�ļ���</param>
        /// <param name="localFolder">����Ŀ¼</param>
        /// <param name="localFileName">�����ļ���</param>
        public bool GetFileNoBinary(string ftpFolder, string ftpFileName, string localFolder, string localFileName)
        {
            try
            {
                if (ftp == null) ftp = this.getFtpClient();
                if (!ftp.Connected)
                {
                    ftp.Connect();
                    ftp.ChDir(ftpFolder);
                }
                ftp.GetNoBinary(ftpFileName, localFolder, localFileName);
                return true;
            }
            catch
            {
                try
                {
                    ftp.DisConnect();
                    ftp = null;
                }
                catch
                {
                    ftp = null;
                }
                return false;
            }
        }

        /// <summary>
        /// �õ�FTP���ļ���Ϣ
        /// </summary>
        /// <param name="ftpFolder">FTPĿ¼</param>
        /// <param name="ftpFileName">ftp�ļ���</param>
        public string GetFileInfo(string ftpFolder, string ftpFileName)
        {
            string strResult = "";
            try
            {
                if (ftp == null) ftp = this.getFtpClient();
                if (ftp.Connected) ftp.DisConnect();
                ftp.Connect();
                ftp.ChDir(ftpFolder);
                strResult = ftp.GetFileInfo(ftpFileName);
                return strResult;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// ����FTP�������Ƿ�ɵ�½
        /// </summary>
        public bool CanConnect()
        {
            if (ftp == null) ftp = this.getFtpClient();
            try
            {
                ftp.Connect();
                ftp.DisConnect();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �õ�FTP���ļ���Ϣ
        /// </summary>
        /// <param name="ftpFolder">FTPĿ¼</param>
        /// <param name="ftpFileName">ftp�ļ���</param>
        public string GetFileInfoConnected(string ftpFolder, string ftpFileName)
        {
            string strResult = "";
            try
            {
                if (ftp == null) ftp = this.getFtpClient();
                if (!ftp.Connected)
                {
                    ftp.Connect();
                    ftp.ChDir(ftpFolder);
                }
                strResult = ftp.GetFileInfo(ftpFileName);
                return strResult;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// �õ��ļ��б�
        /// </summary>
        /// <param name="ftpFolder">FTPĿ¼</param>
        /// <returns>FTPͨ�����</returns>
        public string[] GetFileList(string ftpFolder, string strMask)
        {
            string[] strResult;
            try
            {
                if (ftp == null) ftp = this.getFtpClient();
                if (!ftp.Connected)
                {
                    ftp.Connect();
                    ftp.ChDir(ftpFolder);
                }
                strResult = ftp.Dir(strMask);
                return strResult;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///�õ�FTP�������
        /// </summary>
        public FTPClient getFtpClient()
        {
            FTPClient ft = new FTPClient();
            ft.RemoteHost = this.Server;
            ft.RemoteUser = this.User;
            ft.RemotePass = this.Pass;
            return ft;
        }
    }
}