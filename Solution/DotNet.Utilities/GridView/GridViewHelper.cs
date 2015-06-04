/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
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
        #region ˽�з���
        /// <summary>
        /// ��ȡ���ݳ���
        /// </summary>
        /// <param name="o_Str">ԭ�ַ���</param>
        /// <param name="len">��ȡ����</param>
        /// <returns>��ȡ���ַ���</returns>
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
        /// ��ȡ��Ԫ������
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <returns>����</returns>
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
        /// ���õ�Ԫ������
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <param name="maxLen">��󳤶�</param>
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

        #region ���з���
        /// <summary>
        /// ��GridView����������DataTable
        /// </summary>
        /// <param name="gv">GridView����</param>
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
        /// ��������ת����DataTable
        /// </summary>
        /// <param name="list">����</param>
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
        /// �����ͼ�����ת����DataTable
        /// </summary>
        /// <typeparam name="T">����������</typeparam>
        /// <param name="list">����</param>
        /// <param name="propertyName">��Ҫ���ص��е�����</param>
        /// <returns>���ݼ�(��)</returns>
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

        #region ��������
        #region ��ȡGrid���ѡ���е�Key�¼�

        /// <summary>��ȡgridѡ�е����е�key����ѡʱ����"xx, xxx, xxxx"���ָ�ʽ����</summary>
        /// <param name="grid">Grid�ؼ�</param>
        /// <param name="isMemoryPaging">true���ڴ��ҳ��false=���ݷ�ҳ</param>
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

        /// <summary>��ȡgridѡ�е����е�key</summary>
        /// <param name="grid">Grid�ؼ�</param>
        /// <param name="isMemoryPaging">true���ڴ��ҳ��false=���ݿ��ҳ</param>
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
                //����ڴ��ҳʹ�����:rowIndex = strArray[i] + (grid.PageIndex * grid.PageSize);
                rowIndex = strArray[i] + sp;
                arr[i] = grid.Rows[rowIndex].DataKeys[0];
            }
            return arr;
        }

        /// <summary>��ȡgridѡ�е����е�key</summary>
        /// <param name="grid">Grid�ؼ�</param>
        /// <param name="isMemoryPaging">true���ڴ��ҳ��false=���ݿ��ҳ</param>
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

        /// <summary>��ȡgridѡ�е����е�key(����������󶨶���ֶ�ʱ�����ص�������object���飬�����ֵ�Ƕ���ֶ��ö��ŷָ���ֵ��"["a, b, c", "d, e, f", "g, h, i"......]")</summary>
        /// <param name="grid">Grid�ؼ�</param>
        /// <param name="isMemoryPaging">true���ڴ��ҳ��false=���ݷ�ҳ</param>
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