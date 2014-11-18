/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Reflection;

namespace DotNet.Utilities
{
    public class GridViewHelper
    {
        #region 私有方法
        /// <summary>
        /// 截取内容长度
        /// </summary>
        /// <param name="o_Str">原字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns>截取后字符串</returns>
        private static string GetStrPartly(string o_Str, int len)
        {
            if (len == 0)
            {
                return o_Str;
            }
            else
            {
                if (o_Str.Length > len)
                {
                    return o_Str.Substring(0, len) + "..";
                }
                else
                {
                    return o_Str;
                }
            }
        }

        /// <summary>
        /// 获取单元格内容
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <returns>内容</returns>
        private static string GetCellText(TableCell cell)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            foreach (Control control in cell.Controls)
            {
                if (control != null && control is IButtonControl)
                {
                    IButtonControl btn = control as IButtonControl;
                    text = btn.Text.Replace("\r\n", "").Trim();
                    break;
                }
                if (control != null && control is ITextControl)
                {
                    LiteralControl lc = control as LiteralControl;
                    if (lc != null)
                    {
                        continue;
                    }
                    ITextControl l = control as ITextControl;
                    text = l.Text.Replace("\r\n", "").Trim();
                    break;
                }
            }
            return text;
        }

        /// <summary>
        /// 设置单元格内容
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <param name="maxLen">最大长度</param>
        private static void SetCellText(TableCell cell, int maxLen)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                cell.Text = GetStrPartly(text, maxLen);
            }
            foreach (Control control in cell.Controls)
            {
                if (control != null && control is IButtonControl)
                {
                    IButtonControl btn = control as IButtonControl;
                    text = btn.Text.Replace("\r\n", "").Trim();
                    btn.Text = GetStrPartly(text, maxLen);
                    break;
                }
                if (control != null && control is ITextControl)
                {
                    LiteralControl lc = control as LiteralControl;
                    if (lc != null)
                    {
                        continue;
                    }
                    ITextControl l = control as ITextControl;
                    text = l.Text.Replace("\r\n", "").Trim();
                    if (l is DataBoundLiteralControl)
                    {
                        cell.Text = GetStrPartly(text, maxLen);
                        break;
                    }
                    else
                    {
                        l.Text = GetStrPartly(text, maxLen);
                        break;
                    }
                }
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 从GridView的数据生成DataTable
        /// </summary>
        /// <param name="gv">GridView对象</param>
        public static DataTable GridView2DataTable(GridView gv)
        {
            DataTable table = new DataTable();
            int rowIndex = 0;
            List<string> cols = new List<string>();
            if (!gv.ShowHeader && gv.Columns.Count == 0)
            {
                return table;
            }
            GridViewRow headerRow = gv.HeaderRow;
            int columnCount = headerRow.Cells.Count;
            for (int i = 0; i < columnCount; i++)
            {
                string text = GetCellText(headerRow.Cells[i]);
                cols.Add(text);
            }
            foreach (GridViewRow r in gv.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    DataRow row = table.NewRow();
                    int j = 0;
                    for (int i = 0; i < columnCount; i++)
                    {
                        string text = GetCellText(r.Cells[i]);
                        if (!String.IsNullOrEmpty(text))
                        {
                            if (rowIndex == 0)
                            {
                                string columnName = cols[i];
                                if (String.IsNullOrEmpty(columnName))
                                {
                                    continue;
                                }
                                if (table.Columns.Contains(columnName))
                                {
                                    continue;
                                }
                                DataColumn dc = table.Columns.Add();
                                dc.ColumnName = columnName;
                                dc.DataType = typeof(string);
                            }
                            row[j] = text;
                            j++;
                        }
                    }
                    rowIndex++;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        public static DataTable ToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null) propertyNameList.AddRange(propertyName);

            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name)) result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        #endregion

        #region 其他方法
        #region 获取Grid表格选择行的Key事件

        /// <summary>获取grid选中的行中的key，多选时返回"xx, xxx, xxxx"这种格式数据</summary>
        /// <param name="grid">Grid控件</param>
        /// <param name="isMemoryPaging">true＝内存分页，false=数据分页</param>
        /// <returns></returns>
        public static string GetSelectedKey(FineUI.Grid grid, bool isMemoryPaging = false)
        {
            StringBuilder sb = new StringBuilder();
            int selectedCount = grid.SelectedRowIndexArray.Length;
            int sp = 0;

            if (isMemoryPaging)
            {
                sp = (grid.PageIndex * grid.PageSize);
            }

            for (int i = 0; i < selectedCount; i++)
            {
                int rowIndex = grid.SelectedRowIndexArray[i] + sp;

                object[] dataKeys = grid.DataKeys[rowIndex];
                for (int j = 0; j < dataKeys.Length; j++)
                {
                    sb.Append(dataKeys[j] + ",");
                }
            }
            return StringHelper.FilterSql(sb.ToString().Trim(','));
        }

        /// <summary>获取grid选中的行中的key</summary>
        /// <param name="grid">Grid控件</param>
        /// <param name="isMemoryPaging">true＝内存分页，false=数据库分页</param>
        /// <returns></returns>
        public static object[] GetSelectedKeyArray(FineUI.Grid grid, bool isMemoryPaging = false)
        {
            int[] strArray = grid.SelectedRowIndexArray;
            int ti = strArray.Length;
            if (ti == 0)
            {
                return null;
            }

            var arr = new object[ti];
            int rowIndex = 0;
            int sp = 0;

            if (isMemoryPaging)
            {
                sp = (grid.PageIndex * grid.PageSize);
            }

            for (int i = 0; i < ti; i++)
            {
                //如果内存分页使用这个:rowIndex = strArray[i] + (grid.PageIndex * grid.PageSize);
                rowIndex = strArray[i] + sp;
                arr[i] = grid.Rows[rowIndex].DataKeys[0];
            }
            return arr;
        }

        /// <summary>获取grid选中的行中的key</summary>
        /// <param name="grid">Grid控件</param>
        /// <param name="isMemoryPaging">true＝内存分页，false=数据库分页</param>
        /// <returns></returns>
        public static int[] GetSelectedKeyIntArray(FineUI.Grid grid, bool isMemoryPaging = false)
        {
            var arr = GetSelectedKeyArray(grid, isMemoryPaging);
            if (arr == null || arr.Length == 0)
            {
                return null;
            }
            else
            {
                var intArr = new int[arr.Length];
                for (int i = 0; i < intArr.Length; i++)
                {
                    intArr[i] = ConvertHelper.Cint0(arr[i]);
                }
                return intArr;
            }
        }

        /// <summary>获取grid选中的行中的key(表格中主键绑定多个字段时，返回的内容是object数组，里面的值是多个字段用逗号分隔的值，"["a, b, c", "d, e, f", "g, h, i"......]")</summary>
        /// <param name="grid">Grid控件</param>
        /// <param name="isMemoryPaging">true＝内存分页，false=数据分页</param>
        /// <returns></returns>
        public static object[] GetSelectedKeyAll(FineUI.Grid grid, bool isMemoryPaging = false)
        {
            object[] dataKeys = null;
            int selectedCount = grid.SelectedRowIndexArray.Length;
            int sp = 0;

            if (isMemoryPaging)
            {
                sp = (grid.PageIndex * grid.PageSize);
            }

            for (int i = 0; i < selectedCount; i++)
            {
                int rowIndex = grid.SelectedRowIndexArray[i] + sp;
                dataKeys = grid.DataKeys[rowIndex];
            }
            return dataKeys;
        }


        #endregion

        #endregion
    }
}