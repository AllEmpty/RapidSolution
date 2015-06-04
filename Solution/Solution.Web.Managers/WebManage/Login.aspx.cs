using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：Login.aspx.cs
 *   描    述：后端登陆页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage
{
    public partial class Login : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //生成验证码
                imgCaptcha.ImageUrl = "Application/Vcode.ashx?t=" + DateTime.Now.Ticks;

                #region 初始化用户Session变量
                //清空Session
                SessionHelper.RemoveSession(PositionTable.PagePower);
                SessionHelper.RemoveSession(PositionTable.ControlPower);
                SessionHelper.RemoveSession(OnlineUsersTable.UserHashKey);
                SessionHelper.RemoveSession(OnlineUsersTable.Md5);
                //删除Cookies
                CookieHelper.ClearCookie(OnlineUsersTable.UserHashKey);
                CookieHelper.ClearCookie(OnlineUsersTable.Md5);
                #endregion
            }
        }
        #endregion

        #region 登录
        /// <summary>登录</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            var ip = IpHelper.GetUserIp();

            #region 获取用户输入的参数，并进行数据初步处理
            //获取用户名，并进行危险字符过滤
            var username = StringHelper.Left(txtUserName.Text, 50);
            //获取用户密码
            var userpass = txtPassword.Text;
            //获取验证码
            var strCode = StringHelper.Left(txtCaptcha.Text, 5);
            #endregion

            #region 初步验证
            //开发测试使用，不用每次都输入帐号与密码
            //username = "admin";
            //userpass = "admin";
            //strCode = "12345";

            //用户名验证
            if (string.IsNullOrEmpty(username.Trim()))
            {
                txtUserName.Focus();
                FineUI.Alert.ShowInTop("用户名不能为空,请仔细检查您输入的用户名！", FineUI.MessageBoxIcon.Error);
                return;
            }
            //密码验证
            if (string.IsNullOrEmpty(userpass.Trim()))
            {
                txtPassword.Focus();
                FineUI.Alert.ShowInTop("密码不能为空,请仔细检查您输入的密码！", FineUI.MessageBoxIcon.Error);
                return;
            }

            //验证码验证
            if (string.IsNullOrEmpty(strCode))
            {
                txtCaptcha.Focus();
                FineUI.Alert.ShowInParent("验证码不能为空！", FineUI.MessageBoxIcon.Error);
                return;
            }
            //判断验证码是否正确
            //if (Session["vcode"] == null || !Session["vcode"].ToString().Equals(strCode, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    SessionHelper.RemoveSession("vcode");
            //    txtpass.Focus();
            //    //JsHelper.Alert("验证码错误！");
            //    FineUI.Alert.ShowInParent("验证码错误！", FineUI.MessageBoxIcon.Error);
            //    return;
            //}
            //else
            //{
            //    //验证码正确，删除验证码Session
            //    SessionHelper.RemoveSession("vcode");
            //}
            #endregion

            #region 数据库验证

            //通过用户给的用户名获取相关实体类
            var userinfo = Manager.SingleOrDefault(x => x.LoginName == username);

            //判断用户是否存在
            if (userinfo == null)
            {
                LoginLogBll.GetInstence().Save(0, "账号【" + username + "】不存在，登录失败！");
                txtUserName.Focus();
                FineUI.Alert.ShowInParent("用户名不存在，请仔细检查您输入的用户名！", FineUI.MessageBoxIcon.Error);
                return;
            }

            //密码不匹配
            if (!userinfo.LoginPass.Equals(Encrypt.Md5(Encrypt.Md5(userpass))))
            {
                LoginLogBll.GetInstence().Save(userinfo.Id, "账号【" + userinfo.LoginName + "】的用户【" + userinfo.CName + "】登录失败！登录密码错误。");
                txtPassword.Focus();
                FineUI.Alert.ShowInParent("您输入的用户密码错误！", FineUI.MessageBoxIcon.Error);
                return;
            }

            if (userinfo.IsWork == 0)
            {
                //添加用户登陆日志
                LoginLogBll.GetInstence().Save(userinfo.Id, "离职用户登录失败！用户【" + userinfo.CName + "】试图登录系统");
                FineUI.Alert.ShowInParent("您已经没有权限登录本系统！", FineUI.MessageBoxIcon.Error);
                return;
            }

            //判断当前账号是否被启用
            if (userinfo.IsEnable == 0)
            {
                //添加登录日志记录
                LoginLogBll.GetInstence().Save(userinfo.Id, "账号【" + userinfo.LoginName + "】的用户【" + userinfo.CName + "】登录失败！用户账号被禁用。");

                FineUI.Alert.ShowInParent("当前账号未被启用，请联系管理人员激活！", FineUI.MessageBoxIcon.Error);
                return;
            }

            #endregion

            #region 存储在线用户资料

            #region 获取用户操作权限

            if (string.IsNullOrEmpty(userinfo.Position_Id))
            {
                Session["PagePower"] = "";
                Session["ControlPower"] = "";

                LoginLogBll.GetInstence().Save(0, "账号【" + username + "】未绑定职位，请管理员进行配置！");
                FineUI.Alert.ShowInParent("您的账号未绑定职位，请与管理员联系！", FineUI.MessageBoxIcon.Error);
                return;
            }
            else
            {
                //获取用户权限并存储到用户Session里
                PositionBll.GetInstence().SetUserPower(userinfo.Position_Id);
            }

            #endregion

            #region 当前用户在线信息
            //当前时间
            var localTime = DateTime.Now.ToLocalTime();
            //创建客户端信息获取实体
            var clientHelper = new ClientHelper(Request);

            //创建在线用户实体
            var onlineUser = new OnlineUsers();
            //当前用户的Id编号
            onlineUser.Manager_Id = userinfo.Id;
            onlineUser.Manager_LoginName = userinfo.LoginName;
            onlineUser.Manager_LoginPass = userinfo.LoginPass;
            onlineUser.Manager_CName = userinfo.CName;
            onlineUser.LoginTime = localTime;
            onlineUser.LoginIp = ip;
            //生成密钥
            onlineUser.UserKey = RandomHelper.GetRndNum(32, true);
            //Md5(密钥+登陆帐号+密码+IP+密钥.Substring(6,8))
            onlineUser.Md5 = OnlineUsersBll.GetInstence().GenerateMd5(onlineUser);
            HttpContext.Current.Session[OnlineUsersTable.Md5] = onlineUser.Md5;
            onlineUser.UpdateTime = localTime;
            onlineUser.Sex = userinfo.Sex;
            onlineUser.Branch_Id = userinfo.Branch_Id;
            onlineUser.Branch_Code = userinfo.Branch_Code;
            onlineUser.Branch_Name = userinfo.Branch_Name;
            onlineUser.Position_Id = userinfo.Position_Id;
            onlineUser.Position_Name = userinfo.Position_Name;
            onlineUser.CurrentPage = "";
            onlineUser.CurrentPageTitle = "";
            //SessionId
            onlineUser.SessionId = Session.SessionID;
            onlineUser.UserAgent = StringHelper.FilterSql(HttpContext.Current.Request.Headers["User-Agent"] + "");
            onlineUser.OperatingSystem = clientHelper.GetSystem();
            onlineUser.TerminalType = clientHelper.IsMobileDevice(onlineUser.UserAgent) ? 1 : 0;
            onlineUser.BrowserName = clientHelper.GetBrowserName();
            onlineUser.BrowserVersion = clientHelper.GetBrowserVersion();

            #endregion

            #region 记录当前用户UserId
            //定义HashTable表里Key的名称UserId
            string userHashKey = "";
            //判断当前用户帐户是否支持同一帐号在不同地方登陆功能，取得用户在HashTable表里Key的名称
            //不支持则
            if (userinfo.IsMultiUser == 0)
            {
                userHashKey = userinfo.Id + "";
            }
            //支持则
            else
            {
                userHashKey = userinfo.Id + "_" + onlineUser.SessionId;
            }
            //记录用户的HashTable Key
            onlineUser.UserHashKey = userHashKey;
            Session[OnlineUsersTable.UserHashKey] = userHashKey;
            #endregion

            #region 将在线用户信息存入全局变量中
            //运行在线数据加载函数，如果缓存不存在，则尝试加载数据库中的在线表记录到缓存中
            //——主要用于IIS缓存被应用程序池或其他原因回收后，对在线数据进行重新加载，而不会使所有用户都被迫退出系统
            var onlineUsersList = OnlineUsersBll.GetInstence().GetList();

            //判断缓存中["OnlineUsers"]是否存在，不存在则直接将在线实体添加到缓存中
            if (onlineUsersList == null || onlineUsersList.Count == 0)
            {
                //清除在线表里与当前用户同名的记录
                OnlineUsersBll.GetInstence().Delete(this, x => x.Manager_LoginName == onlineUser.Manager_LoginName);

                //将在线实体保存到数据库的在线表中
                OnlineUsersBll.GetInstence().Save(this, onlineUser, null, true, false);
            }
            //存在则将它取出HashTable并进行处理
            else
            {
                //将HashTable里存储的前一登陆帐户移除
                //获取在线缓存实体
                var onlineModel = OnlineUsersBll.GetInstence().GetOnlineUsersModel(userHashKey);
                if (onlineModel != null)
                {
                    //添加用户下线记录
                    LoginLogBll.GetInstence().Save(userHashKey, "用户【{0}】的账号已经在另一处登录，本次登陆下线！在线时间【{1}】");

                    //清除在线表里与当前用户同名的记录
                    OnlineUsersBll.GetInstence().Delete(this, x => x.Manager_Id == onlineUser.Manager_Id);
                }

                //将在线实体保存到数据库的在线表中
                OnlineUsersBll.GetInstence().Save(this, onlineUser, null, true, false);
            }

            //检查在线列表数据，将不在线人员删除
            OnlineUsersBll.GetInstence().CheckOnline();

            #endregion

            #endregion

            #region 更新用户登陆信息

            userinfo.LoginIp = ip;
            userinfo.LoginCount = ++userinfo.LoginCount;
            userinfo.LoginTime = localTime;

            ManagerBll.GetInstence().Save(this, userinfo, string.Format("用户【{0}】登陆成功，更新登陆信息", userinfo.CName));

            #endregion

            #region 添加用户登录成功日志
            LoginLogBll.GetInstence().Save(userHashKey, string.Format("账号【{0}】的用户【{1}】登录成功", userinfo.LoginName, userinfo.CName));
            #endregion

            #region 写Cookies
            //写入用户的HashTable Key
            CookieHelper.SetCookie(OnlineUsersTable.UserHashKey, userHashKey);
            //写入加密值
            CookieHelper.SetCookie(OnlineUsersTable.Md5, onlineUser.Md5);
            #endregion

            //跳转进入主页面           
            Response.Redirect("Main.aspx");
        }
        #endregion

        #region 刷新验证码
        /// <summary>刷新验证码</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //重新生成验证码
            imgCaptcha.ImageUrl = "Application/Vcode.ashx?t=" + DateTime.Now.Ticks;
        }
        #endregion

    }
}