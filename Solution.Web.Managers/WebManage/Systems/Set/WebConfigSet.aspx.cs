using System;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;


/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-24
 *   文件名称：WebConfigEdit.aspx.cs
 *   描    述：系统配置编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Set
{
    public partial class WebConfigSet : PageBase
    {
        private int id = 1;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //加载数据
                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {

        }
        #endregion

        #region 加载数据
        /// <summary>读取数据</summary>
        public override void LoadData()
        {
            //获取指定ID的系统配置内容
            var model = WebConfigBll.GetInstence().GetModelForCache(x => x.Id == id);
            if (model == null)
                return;

            txtWebName.Text = model.WebName;
            txtWebDomain.Text = model.WebDomain;
            txtWebEmail.Text = model.WebEmail;

            //--------------------------------------------
            txtLoginLogReserveTime.Text = model.LoginLogReserveTime + "";
            txtUseLogReserveTime.Text = model.UseLogReserveTime + "";

            //--------------------------------------------
            txtEmailSmtp.Text = model.EmailSmtp;
            txtEmailUserName.Text = model.EmailUserName;
            txtEmailPassWord.Text = model.EmailPassWord;
            
        }

        #endregion
        
        #region 保存
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <returns></returns>
        public override string Save()
        {
            string result = string.Empty;

            try
            {
                #region 赋值

                //获取实体
                var model = new WebConfig(x => x.Id == id);
                model.WebName = StringHelper.Left(txtWebName.Text, 50);
                model.WebDomain = StringHelper.Left(txtWebDomain.Text, 50, true, false);
                model.WebEmail = StringHelper.Left(txtWebEmail.Text, 50, true, false);

                model.LoginLogReserveTime = ConvertHelper.Cint0(txtLoginLogReserveTime.Text);
                model.UseLogReserveTime = ConvertHelper.Cint0(txtUseLogReserveTime.Text);

                model.EmailSmtp = StringHelper.Left(txtEmailSmtp.Text, 50, true, false);
                model.EmailUserName = StringHelper.Left(txtEmailUserName.Text, 50);
                model.EmailPassWord = StringHelper.Left(txtEmailPassWord.Text, 50, true, false);

                #endregion

                //----------------------------------------------------------
                //存储到数据库
                WebConfigBll.GetInstence().Save(this, model);

                //------------------------------------
                //测试邮件发送服务
                if (chkSendTest.Checked && model.EmailSmtp.Length > 0 && model.EmailUserName.Length > 0)
                {
                    var oMail = new MailBll();
                    string ss = oMail.TestMail();

                    if (ss.Length > 0)
                    {
                        return ("出错！" + ss);
                    }
                    else
                    {
                        return ("发送成功！");
                    }
                }
                return "修改成功！";
            }
            catch (Exception e)
            {
                result = "保存失败！";

                //出现异常，保存出错日志信息
                CommonBll.WriteLog(result, e);
            }

            return result;
        }
        #endregion
    }
}