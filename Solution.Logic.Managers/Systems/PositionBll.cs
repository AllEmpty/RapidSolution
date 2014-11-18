using System;
using System.Web;
using DotNet.Utilities;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：PositionBll.cs
 *   描    述：职位（角色）管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// Position表逻辑类
    /// </summary>
    public partial class PositionBll : LogicBase
    {
        /***********************************************************************
         * 自定义函数                                                          *
         ***********************************************************************/
        #region 自定义函数

        #region 获取用户权限并记录到用户Session里
        /// <summary>
        /// 获取用户权限并存储到用户Session里
        /// </summary>
        /// <param name="positionId"></param>
        public void SetUserPower(string positionId)
        {
            if (!string.IsNullOrEmpty(positionId))
            {
                //去掉两边的逗号
                positionId = StringHelper.DelStrSign(positionId);

                //因用户有的拥有多个职位，所以将用户职位取出并存入数组
                string[] arr = positionId.Split(new char[] { ',' });

                //循环读取用户职位权限
                for (int i = 0; i < arr.Length; i++)
                {
                    var id = ConvertHelper.Cint0(arr[i]);
                    //取得职位实体对象
                    var model = GetModelForCache(x => x.Id == id);
                    if (model != null)
                    {
                        //将用户权限记录到用户Session里
                        SetPagePower(model.PagePower);
                        SetControlPower(model.ControlPower);
                    }
                }
            }
        }

        /// <summary>
        /// 将用户的页面访问权限记录到Session["PagePower"]里
        /// </summary>
        /// <param name="pagePower">页面访问权限</param>
        private void SetPagePower(string pagePower)
        {
            //如果页面访问权限Session为空，则直接赋值
            if (HttpContext.Current.Session["PagePower"] == null)
            {
                HttpContext.Current.Session["PagePower"] = pagePower;
            }
            else
            {
                //从Session中读取出已存储的权限字串
                string spp = HttpContext.Current.Session["PagePower"] + "";

                //将传入的变量存入数组pp
                string[] pp = pagePower.Split(new char[] { ',' });
                //循环逐个判断权限是否存在
                for (int i = 0; i < pp.Length; i++)
                {
                    //权限不存在的，则加入该权限
                    if (spp.IndexOf("," + pp[i] + ",") < 0 && pp[i] != "")
                    {
                        spp += pp[i] + ",";
                    }
                }
                //将添加了其他职位权限后的权限字符串存入Session
                HttpContext.Current.Session["PagePower"] = spp;
            }
        }

        /// <summary>
        /// 将用户页面的控件访问权限记录到Session["ControlPower"]里
        /// </summary>
        /// <param name="controlPower">页面的控件访问权限</param>
        private void SetControlPower(string controlPower)
        {
            //如果页面访问权限Session为空，则直接赋值
            if (HttpContext.Current.Session["ControlPower"] == null)
            {
                HttpContext.Current.Session["ControlPower"] = controlPower;
            }
            else
            {
                //从Session中读取出已存储的权限字串
                string spp = Convert.ToString(HttpContext.Current.Session["ControlPower"]);

                //将传入的变量存入数组pp
                string[] pp = controlPower.Split(new char[] { '|' });
                //循环逐个判断权限是否存在
                for (int i = 0; i < pp.Length; i++)
                {
                    //权限不存在的，则加入该权限
                    if (spp.IndexOf("|" + pp[i] + "|") < 0 && pp[i] != "")
                    {
                        spp += pp[i] + "|";
                    }
                }
                //将添加了其他职位权限后的权限字符串存入Session
                HttpContext.Current.Session["ControlPower"] = spp;
            }
        }
        #endregion

        #region 获取职位名称
        /// <summary>
        /// 获取职位名称
        /// </summary>
        /// <param name="positionId">职位Id字符串</param>
        /// <returns></returns>
        public string GetName(string positionId)
        {
            string name = "";

            if (!string.IsNullOrEmpty(positionId))
            {
                //去掉两边的逗号
                positionId = StringHelper.DelStrSign(positionId);

                //因用户有的拥有多个职位，所以将用户职位取出并存入数组
                string[] arr = StringHelper.GetArrayStr(positionId);

                //循环读取用户职位权限
                for (int i = 0; i < arr.Length; i++)
                {
                    var id = ConvertHelper.Cint0(arr[i]);
                    //取得职位实体对象
                    var model = GetModelForCache(x => x.Id == id);
                    if (model != null)
                    {
                        name += model.Name + ", ";
                    }
                }
            }

            //去除后面的逗号
            if (!string.IsNullOrEmpty(name))
            {
                name = StringHelper.DelLastComma(name);
            }

            return name;
        }
        #endregion

        #endregion 自定义函数

    }
}
