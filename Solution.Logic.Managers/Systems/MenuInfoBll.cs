using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：MenuInfoBll.cs
 *   描    述：后端菜单管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// MenuInfoBll逻辑类
    /// </summary>
    public partial class MenuInfoBll : LogicBase
    {
        /***********************************************************************
		 * 自定义函数                                                          *
		 ***********************************************************************/

        #region 自定义函数

        private const string const_CacheKey_Model = "Cache_MenuInfo_AllModel";

        #region 获取MenuInfo全表内容并放到缓存中

        /// <summary>
        /// 取得MenuInfo全表内容——使用菜单地址做为KEY
        /// </summary>
        /// <returns>返回Hashtable</returns>
        public Hashtable GetHashtable()
        {
            //读取记录
            object obj = CacheHelper.GetCache(const_CacheKey_Model);
            //如果记录不存在，则重新加载
            if (obj == null)
            {
                //初始化全局菜单内容缓存
                var ht = new Hashtable();
                //获取菜单表全部内容
                var all = MenuInfo.All();
                //遍历读取
                foreach (var model in all)
                {
                    //创建菜单实体
                    var menuinfo = new MenuInfo();
                    menuinfo.Id = model.Id;
                    menuinfo.Name = model.Name;
                    menuinfo.Url = model.Url;
                    menuinfo.ParentId = model.ParentId;
                    menuinfo.Depth = model.Depth;

                    try
                    {
                        //将菜单实体存入容器中
                        //使用页面地址做为KEY
                        ht.Add(menuinfo.Url, menuinfo);
                    }
                    catch (Exception)
                    {
                    }
                }

                if (ht.Count > 0)
                {
                    CacheHelper.SetCache(const_CacheKey_Model, ht);
                }

                return ht;
            }
            else
            {
                return (Hashtable)obj;
            }
        }

        #endregion

        #region 清空缓存

        /// <summary>清空缓存</summary>
        public override void DelCache()
        {
            CacheHelper.RemoveOneCache(const_CacheKey_Model);
        }

        #endregion

        #region 根据菜单Url地址，获取菜单相应内容

        /// <summary>
        /// 根据菜单Url地址，获取菜单相应内容
        /// </summary>
        /// <param name="menuInfoUrl">页面地址</param>
        /// <returns>返回菜单实体</returns>
        public MenuInfo GetMenuInfo(string menuInfoUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(menuInfoUrl))
                    return null;

                //从全局缓存中读取菜单内容
                //获取菜单实体
                return (MenuInfo)(MenuInfoBll.GetInstence().GetHashtable()[menuInfoUrl]);
            }
            catch (Exception)
            {
                return new MenuInfo();
            }
        }

        #endregion

        #region 检查页面权限

        /// <summary>
        /// 检查当前菜单或页面是否有权限访问
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>真或假</returns>
        public bool CheckPagePower(string menuId)
        {
            var pagePower = OnlineUsersBll.GetInstence().GetPagePower();
            if (string.IsNullOrEmpty(pagePower) || menuId == "")
            {
                return false;
            }
            //检查是否有权限
            if (
                pagePower.IndexOf("," + menuId + ",") >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 检查当前页面控件是否有权限访问

        /// <summary>
        /// 检查当前页面控件是否有权限访问
        /// </summary>
        /// <param name="page">页面指针</param>
        /// <param name="controlName">控件名称</param>
        /// <returns>真或假</returns>
        public bool CheckControlPower(Page page, string controlName)
        {
            //获取当前访问页面的URL
            var currentPage = page.Request.Url.AbsolutePath;
            //获取当前用户所有可以访问的页面ID
            var menuId = GetMenuInfo(currentPage).Id;
            //判断全局缓存中是否存储了该控件ID,否的话表示该控件没有权限
            if (PagePowerSignPublicBll.GetInstence().GetHashtable()[controlName] == null)
            {
                return false;
            }
            else
            {
                var controlPower = OnlineUsersBll.GetInstence().GetControlPower();
                if (string.IsNullOrEmpty(controlPower))
                {
                    return false;
                }
                //获取当前控件ID
                string ppsID = PagePowerSignPublicBll.GetInstence().GetHashtable()[controlName].ToString();

                //检查是否有权限
                if (controlPower.IndexOf("," + menuId + "|" + ppsID + ",") >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        #endregion

        #region 判断当前用户是否有当前页面操作权限

        /// <summary>
        /// 判断当前用户是否有本页面的操作权限
        /// </summary>
        /// <returns></returns>
        public void CheckPagePower(Page page)
        {
            try
            {
                //获取当前访问页面的名称
                var currentPage = page.Request.Url.AbsolutePath;
                if (currentPage.Equals("/WebManage/Main.aspx"))
                    return;

                //检查是否从正确路径进入
                CheckPageEncrypt(page);


                //获取当前用户所有可以访问的页面ID
                var menuId = GetMenuInfo(currentPage).Id + "";

                if (!CheckPagePower(menuId))
                {
                    //添加用户访问记录
                    UseLogBll.GetInstence().Save(page, "{0}没有权限访问【{1}】页面");

                    page.Response.Write("您没有访问该页面的权限！");
                    page.Response.End();
                    return;
                }

            }
            catch (Exception e)
            {
                // 记录日志
                CommonBll.WriteLog("绑定表格时出现异常", e);

                //添加用户访问记录
                UseLogBll.GetInstence().Save(page, "{0}没有权限访问【{1}】页面");

                page.Response.Write("您没有访问该页面的权限！");
                page.Response.End();
                return;
            }
        }

        #endregion

        #region 页面访问加密--检查用户是否从正确的路径进入本页面

        /// <summary>
        /// 设置页面加密--用于检查用户是否从正确的路径进入本页面
        /// </summary>
        /// <param name="key">页面加密的Key</param>
        /// <returns>加密后的字符串</returns>
        public string SetPageEncrypt(string key)
        {
            //当前用户md5
            var md5 = OnlineUsersBll.GetInstence().GetMd5();
            //加密：md5+Key
            var encrypt = DotNet.Utilities.Encrypt.Md5(md5 + key);
            //再次加密：Key + Encrypt
            return Encrypt.Md5(key + encrypt);
        }

        /// <summary>
        /// 检查用户是否从正确的路径进入本页面，默认KEY为ID
        /// </summary>
        public void CheckPageEncrypt(Page page)
        {
            //当前用户md5
            var md5 = OnlineUsersBll.GetInstence().GetMd5();
            //Key，如果没有传递Key这个变量过来的，就读取id或ParentID做为Key使用
            var key = HttpContext.Current.Request["Id"];
            if (string.IsNullOrEmpty(key))
            {
                key = HttpContext.Current.Request["pid"];
            }
            if (string.IsNullOrEmpty(key))
            {
                key = HttpContext.Current.Request["ParentId"];
            }
            if (string.IsNullOrEmpty(key))
            {
                key = HttpContext.Current.Request["Key"];
            }
            //上一链接传过来的加密数据
            var keyEncrypt = HttpContext.Current.Request["KeyEncrypt"];

            //加密：md5+Key
            var encrypt = Encrypt.Md5(md5 + key);
            //再次加密：Key + Encrypt
            encrypt = Encrypt.Md5(key + encrypt);

            //检查是否有权限，没有权限的直接终止当前页面的运行
            if (keyEncrypt != encrypt || string.IsNullOrEmpty(key))
            {
                //添加用户访问记录
                UseLogBll.GetInstence().Save(page, "{0}没有权限访问【{1}】页面");

                HttpContext.Current.Response.Write("你从错误的路径进入当前页面！");
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 组成URL加密参数字符串
        /// </summary>
        /// <param name="key">页面加密的Key</param>
        /// <returns>组成URL加密参数字符串</returns>
        public string PageUrlEncryptString(string key)
        {
            return "KeyEncrypt=" + SetPageEncrypt(key) + "&Key=" + key;
        }

        /// <summary>
        /// 组成URL加密参数字符串，使用随机生成的Key，如果页面传的参数中包含有ID这个名称的，则不能使用本函数
        /// </summary>
        /// <returns>组成URL加密参数字符串</returns>
        public string PageUrlEncryptString()
        {
            var key = RandomHelper.GetRandomCode(null, 12);
            return "KeyEncrypt=" + SetPageEncrypt(key) + "&Id=" + key;
        }

        /// <summary>
        /// 组成URL加密参数字符串——返回不带Key的字符串
        /// </summary>
        /// <param name="key">页面加密的Key</param>
        /// <returns>组成URL加密参数字符串——不带Key</returns>
        public string PageUrlEncryptStringNoKey(string key)
        {
            return "KeyEncrypt=" + SetPageEncrypt(key);
        }

        /// <summary>和 PageBase.BtnSave_Click 对应，部分页面刷新后不关闭原页面，并要刷新的情况下使用</summary>
        /// <param name="url">跳转的url</param>
        /// <returns></returns>
        public string PageSaveReturnUrlFlash(string url)
        {
            //url = DirFileHelper.GetFilePath(HttpContext.Current.Request.Path) + "/" + url;
            return "{url}" + url;
        }

        #endregion

        #region 从页面中找到放置按键控件的位置——获得一个控件组

        /// <summary>
        /// 从页面中找到放置按键控件的位置——获得一个控件组
        /// </summary>
        /// <param name="controls"></param>
        /// <returns></returns>
        public ControlCollection GetControls(ControlCollection controls, string id)
        {
            if (controls == null)
                return null;

            ControlCollection c = null;
            try
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    if (controls[i].ID == id)
                    {
                        return controls[i].Controls;
                    }
                    c = GetControls(controls[i].Controls, id);
                    if (c != null)
                        return c;
                }
            }
            catch (Exception e)
            {
                // 记录日志
                CommonBll.WriteLog("", e);
            }

            return c;
        }

        /// <summary>
        /// 从页面中找到放置按键控件的位置
        /// </summary>
        /// <param name="controls"></param>
        /// <returns></returns>
        public object FindControl(ControlCollection controls, string id)
        {
            if (controls == null)
                return null;

            object c = null;
            try
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    if (controls[i].ID == id)
                    {
                        return controls[i];
                    }
                    c = FindControl(controls[i].Controls, id);
                    if (c != null)
                        return c;
                }
            }
            catch (Exception e)
            {
                // 记录日志
                CommonBll.WriteLog("", e);
            }

            return c;
        }
        #endregion

        #region 绑定菜单下拉列表
        /// <summary>
        /// 绑定菜单下拉列表——只显示一级菜单
        /// </summary>
        public void BandDropDownList(Page page, FineUI.DropDownList ddl)
        {
            var dt = DataTableHelper.GetFilterData(GetDataTable(), MenuInfoTable.ParentId, "0", MenuInfoTable.Sort, "desc");

            //显示值
            ddl.DataTextField = MenuInfoTable.Name;
            //显示key
            ddl.DataValueField = MenuInfoTable.Id;

            //绑定数据源
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new FineUI.ListItem("请选择菜单", "0"));
            ddl.SelectedValue = "0";
        }

        /// <summary>
        /// 绑定菜单下拉列表——只显示所有可以显示的菜单（IsMenu)
        /// </summary>
        public void BandDropDownListShowMenu(Page page, FineUI.DropDownList ddl)
        {
            //在内存中筛选记录
            var dt = DataTableHelper.GetFilterData(GetDataTable(), string.Format("{0}={1}", MenuInfoTable.IsMenu, 0), MenuInfoTable.Depth + ", " + MenuInfoTable.Sort);

            try
            {
                //整理出有层次感的数据
                dt = DataTableHelper.DataTableTidyUp(dt, MenuInfoTable.Id, MenuInfoTable.ParentId, 0);

                ddl.EnableSimulateTree = true;

                //显示值
                ddl.DataTextField = MenuInfoTable.Name;
                //显示key
                ddl.DataValueField = MenuInfoTable.Id;
                //数据层次
                ddl.DataSimulateTreeLevelField = MenuInfoTable.Depth;
                //绑定数据源
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.SelectedIndex = 0;

                ddl.Items.Insert(0, new FineUI.ListItem("请选择菜单", "0"));
                ddl.SelectedValue = "0";
            }
            catch (Exception e)
            {
                // 记录日志
                CommonBll.WriteLog("", e);
            }
        }
        #endregion
        
        #endregion 自定义函数
    }
}
