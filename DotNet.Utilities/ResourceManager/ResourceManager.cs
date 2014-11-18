/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DotNet.Utilities
{
    /// <summary>
    /// ResourceManager
    /// </author> 
    /// </summary>
    public class ResourceManager
    {
        private volatile static ResourceManager instance = null;
        private static object locker = new Object();
        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ResourceManager();
                        }
                    }
                }
                return instance;
            }
        }

        private string FolderPath = string.Empty;
        public SortedList<String, Resources> LanguageResources = new SortedList<String, Resources>();

        public void Serialize(Resources resources, string filePath)
        {
            ResourcesSerializer.Serialize(filePath, resources);
        }

        public void Init(string filePath)
        {
            FolderPath = filePath;
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            LanguageResources.Clear();
            if (!directoryInfo.Exists)
            {
                return;
            }
            FileInfo[] FileInfo = directoryInfo.GetFiles();
            for (int i = 0; i < FileInfo.Length; i++)
            {
                Resources resources = ResourcesSerializer.DeSerialize(FileInfo[i].FullName);
                resources.createIndex();
                LanguageResources.Add(resources.language, resources);     
            }
        }

        public Hashtable GetLanguages()
        {
            Hashtable hashtable = new Hashtable();
            IEnumerator<KeyValuePair<String, Resources>> iEnumerator = LanguageResources.GetEnumerator();
            while (iEnumerator.MoveNext())
            {
                hashtable.Add(iEnumerator.Current.Key, iEnumerator.Current.Value.displayName);
            }
            return hashtable;
        }

        public Hashtable GetLanguages(string path)
        {
            Hashtable hashtable = new Hashtable();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                return hashtable;
            }
            FileInfo[] fileInfo = directoryInfo.GetFiles();
            for (int i = 0; i < fileInfo.Length; i++)
            {
                Resources resources = ResourcesSerializer.DeSerialize(fileInfo[i].FullName);
                hashtable.Add(resources.language, resources.displayName);
            }
            return hashtable;
        }

        public Resources GetResources(string filePath)
        {
            Resources resources = new Resources();
            if (File.Exists(filePath))
            {
                resources = ResourcesSerializer.DeSerialize(filePath);
                resources.createIndex();
            }
            return resources;
        }
                
        public string Get(string language, string key)
        {
            if (!LanguageResources.ContainsKey(language))
            {
                return string.Empty;
            }
            return LanguageResources[language].Get(key);
        }
    }
}