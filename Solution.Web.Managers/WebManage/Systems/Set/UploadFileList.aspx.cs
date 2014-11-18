using System;
using DotNet.Utilities;
using FineUI;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-27
 *   文件名称：UploadFileList.aspx.cs
 *   描    述：上传文件列表管理
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Set
{
    public partial class UploadFileList : PageBase
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {
            //逻辑对象赋值
            bll = UploadFileBll.GetInstence();
            //表格对象赋值
            grid = Grid1;
        }
        #endregion

        #region 加载数据
        /// <summary>读取数据</summary>
        public override void LoadData()
        {
            //设置排序
            if (sortList == null)
            {
                Sort(null);
            }

            //绑定Grid表格
            bll.BindGrid(Grid1, Grid1.PageIndex + 1, Grid1.PageSize, null, sortList);
        }

        #endregion
        
        #region 列表属性绑定

        #region 列表按键绑定——修改列表控件属性
        /// <summary>
        /// 列表按键绑定——修改列表控件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            //绑定用户类型
            GridRow gr = Grid1.Rows[e.RowIndex];
            if (((System.Data.DataRowView)(gr.DataItem)).Row.Table.Rows[e.RowIndex][UploadFileTable.UserType].ToString() == "1")
            {
                var lbf = Grid1.FindColumn("UserType") as LinkButtonField;
                lbf.Text = "管理员";
                lbf.Enabled = false;
            }
            else
            {
                var lbf = Grid1.FindColumn("UserType") as LinkButtonField;
                lbf.Text = "会员";
                lbf.Enabled = false;
            }
        }
        #endregion
        
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public override string Delete()
        {
            //获取要删除的Id组
            var id = GridViewHelper.GetSelectedKeyIntArray(Grid1);
            
            //如果没有选择记录，则直接退出
            if (id == null)
            {
                return "请选择要删除的记录。";
            }

            try
            {
                //逐个文件删除
                foreach (var i in id)
                {
                    //获取文件路径
                    var path = UploadFileBll.GetInstence().GetFieldValue(i, UploadFileTable.Path) + "";
                    //删除文件与对应的记录
                    UploadFileBll.GetInstence().Upload_OneDelPic(path);
                    //删除记录
                    bll.Delete(this, i);
                }
                
                return "删除编号Id为[" + string.Join(",", id) + "]的数据记录成功。";
            }
            catch (Exception e)
            {
                string result = "尝试删除编号ID为[" + string.Join(",", id) +"]的数据记录失败！";

                //出现异常，保存出错日志信息
                CommonBll.WriteLog(result, e);

                return result;
            }
        }
        #endregion

        #region 全部图片重新生成
        /// <summary>
        /// 全部图片重新生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonImageRegenerate_Click(object sender, EventArgs e)
        {
            try
            {
                UploadFileBll.GetInstence().fix_PicSizeAll();
                FineUI.Alert.ShowInParent("全部图片重新生成成功", FineUI.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //出现异常，保存出错日志信息
                CommonBll.WriteLog("重新生成图片失败", ex);
                FineUI.Alert.ShowInParent("重新生成图片失败", FineUI.MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 检查扩展名，判断是否是图片
        /// <summary>
        /// 检查扩展名，判断是否是图片
        /// </summary>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        protected string CheckPic(object ext, object path)
        {
            if (ext == null || path == null) return "";

            if (",jpg,gif,png,".IndexOf("," + ext + ",") > -1)
            {
                return "<img src=\"" + path + "\" />"; ;
            }

            return path.ToString();
        }
        #endregion
    }
}