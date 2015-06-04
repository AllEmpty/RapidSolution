using System;
using System.Data;
using System.Text;

namespace DotNet.Utilities
{
    /// <summary>
    /// 对DataTable进行处理
    /// </summary>
    public class DataTableHelper
    {

        #region 从DataTable查找指定值
        /// <summary>从DataTable查找指定值</summary>
        /// <param name="dt">要查找的DataTable</param>
        /// <param name="where">条件</param>
        /// <param name="retField">返回值的字段名</param>
        /// <returns></returns>
        public static object DataTable_Find_Value(DataTable dt, string where, string retField)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }

            if (string.IsNullOrEmpty(where) || string.IsNullOrEmpty(retField))
            {
                return "";
            }

            DataRow[] foundRows = dt.Select(where);
            int ti = foundRows.Length;
            string ret = "";

            if (ti > 0)
            {
                return foundRows[0][retField];
                
            }

            return null;
        }

        /// <summary>从DataTable查找指定行</summary>
        /// <param name="dt">要查找的DataTable</param>
        /// <param name="str">比较条件：当前的值，比如：ClassID = 1</param>
        /// <param name="findField">查找的字段："ClassID"</param>
        /// <returns></returns>
        public static DataRow DataTable_SelectDataRow(DataTable dt, string str, string findField)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            int ti = dt.Rows.Count;
            for (int i = 0; i < ti; i++)
            {
                if (dt.Rows[i][findField].ToString() == str)
                {
                    return dt.Rows[i];
                }
            }
            return null;
        }

        /// <summary>从DataTable查找指定id list</summary>
        /// <param name="dt">要查找的DataTable</param>
        /// <param name="sWhere">在dataTable中查找到定条件的记录，并返回新的DataTable，例如： IsPost=1 and IsShow=1</param>
        /// <param name="retField">返回的字段："ClassID"</param>
        /// <returns></returns>
        public static string DataTable_GetIdList(DataTable dt, string sWhere, string retField)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }

            if (string.IsNullOrEmpty(sWhere) || string.IsNullOrEmpty(retField))
            {
                return "";
            }

            DataRow[] foundRows = dt.Select(sWhere);
            int ti = foundRows.Length;
            string ret = "";

            if (ti > 0)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < ti; i++)
                {
                    sb.Append(foundRows[i][retField]);
                    sb.Append(",");
                }
                ret = sb.ToString().Trim(',');
            }
            return ret;
        }

        #endregion

        #region 筛选函数，将数据表里面指定的值查找出来
        /// <summary>
        /// 在dataTable中查找到定条件的记录，并返回新的DataTable
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="colName">要找查的名称（条件名，为空时表示查询全部）</param>
        /// <param name="colValue">要查找的值</param>
        /// <param name="sortName">排序字段名</param>
        /// <param name="orderby">升序或降序（Asc/Desc）</param>
        /// <returns>返回筛选后的数据表</returns>
        public static DataTable GetFilterData(DataTable dt, string colName, string colValue, string sortName, string orderby)
        {
            var wheres = string.IsNullOrEmpty(colName) ? "" : colName + "=" + colValue;
            string sort = null;
            if (!string.IsNullOrEmpty(sortName))
            {
               sort = sortName + " " + orderby;
            }
            return GetFilterData(dt, wheres, sort);
        }


        /// <summary>
        /// 筛选函数，将数据表里面指定的值查找出来
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="wheres">条件，例：Id=100 and xx=20</param>
        /// <param name="sort">排序，例：Id Desc</param>
        /// <returns>返回筛选后的数据表</returns>
        public static DataTable GetFilterData(DataTable dt, string wheres, string sort)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            try
            {
                DataTable _dt = null;
                DataRow[] drs = null;
                //查询
                if (!string.IsNullOrEmpty(wheres))
                {
                    //内存表中查询数据
                    drs = dt.Select(wheres);
                    //CopyToDataTable 必须 引用 System.Data.DataSetExtensions
                    _dt = drs.Length > 0 ? drs.CopyToDataTable() : dt.Clone();
                }
                //设置排序
                if (!string.IsNullOrEmpty(sort))
                {
                    _dt.DefaultView.Sort = sort;
                    _dt = _dt.DefaultView.ToTable();
                }

                dt.Dispose();
                return _dt;
            }
            catch { }

            return null;
        }
        #endregion

        #region 取得数组
        /// <summary>根据DataTable,返回指定列数据列表，用“，”进行分隔</summary>
        /// <param name="dt">DataTable</param>
        /// <param name="colName">列名</param>
        /// <returns></returns>
        public static string GetColList(DataTable dt, string colName)
        {
            string sRet = "";
            if (dt == null || dt.Rows.Count == 0)
            {
                return sRet;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int t = dt.Rows.Count;
            for (int i = 0; i < t; i++)
            {
                sb.Append(dt.Rows[i][colName].ToString() + ",");
            }
            dt.Dispose();

            sRet = sb.ToString();
            if (sRet.Length > 0)
            {
                return StringHelper.DelLastComma(sRet);
            }
            return sRet;
        }

        /// <summary>根据DataTable,返回指定列数据的string[]</summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static string[] GetArrayString(DataTable dt, string colName)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return new string[0] { };
            }

            int t = dt.Rows.Count;
            string[] arr = new string[t];
            for (int i = 0; i < t; i++)
            {
                arr[i] = dt.Rows[i][colName].ToString();
            }
            dt.Dispose();
            return arr;
        }

        /// <summary>根据DataTable,返回指定列数据的int[]</summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static int[] GetArrayInt(DataTable dt, string colName)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return new int[0] { };
            }

            int t = dt.Rows.Count;
            var arr = new int[t];
            for (int i = 0; i < t; i++)
            {
                arr[i] = ConvertHelper.Cint0(dt.Rows[i][colName].ToString());
            }
            dt.Dispose();
            return arr;
        }

        /// <summary>根据DataTable,返回第一行,各列数据到string[]</summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static string[] GetColumnsString(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return new string[0] { };
            }

            int ti = dt.Columns.Count;
            string[] arr = new string[ti];
            for (int i = 0; i < ti; i++)
            {
                arr[i] = dt.Rows[0][i].ToString();
            }
            dt.Dispose();
            return arr;
        }

        /// <summary>根据DataTable,返回n行n列的数据到string[,]</summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static string[,] GetArrayString(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                string[,] a2 = new string[0, 2];
                return a2;
            }

            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;

            string[,] arr = new string[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr[i, j] = dt.Rows[i][j].ToString();
                }
            }
            dt.Dispose();
            return arr;
        }

        #endregion

        #region 整理dataTable数据，以便于在有层次感的数据容器中使用
        /// <summary>整理dataTable数据，以便于在有层次感的数据容器中使用
        /// </summary>
        /// <param name="dtable">DataTable数据源</param>
        /// <param name="pkFiled">主键ID列名</param>
        /// <param name="parentIdFiled">父级ID列名</param>
        /// <returns></returns>
        public static DataTable DataTableTidyUp(DataTable dtable, string pkFiled, string parentIdFiled)
        {
            return DataTableTidyUp(dtable, pkFiled, parentIdFiled, 0);
        }

        /// <summary>
        /// 整理dataTable数据，以便于在有层次感的数据容器中使用
        /// </summary>
        /// <param name="dtable">DataTable数据源</param>
        /// <param name="pkFiled">主键ID列名</param>
        /// <param name="parentIdFiled">父级ID列名</param>
        /// <param name="parentId">父ID值，用于查询分类列表时，只显示指定一级分类下面的全部分类</param>
        /// <returns></returns>
        public static DataTable DataTableTidyUp(DataTable dtable, string pkFiled, string parentIdFiled, int parentId)
        {

            //判断当前内存表中是否存在指定的主键列
            if (!dtable.Columns.Contains(pkFiled) || !dtable.Columns.Contains(parentIdFiled))
            {
                //不存在指定的主键列
                return null;
            }
            //设定主键列
            dtable.PrimaryKey = new DataColumn[] { dtable.Columns[pkFiled] };

            //克隆内存表中的结构与约束
            DataTable tidyUpdata = dtable.Clone();

            //父ID列表，用于使用条件查询时，只将指定父ID节点（根节点）以及它下面的子节点显示出来，其他节点不显示
            string parentIDList = ",";

            //循环读取表中的记录
            foreach (DataRow item in dtable.Rows)
            {
                //获取父ID值
                int pid = int.Parse(item[parentIdFiled].ToString());
                //判断当前的父ID是否为0（即是否是根节点），为0则直接加入,否则寻找其父id的位置
                if (pid == 0)
                {
                    //如果指定了只显示指定根节点以及它的子节点，则判断当前父节点是否为指定的父节点，不是则终止本次循环
                    if (parentId > 0 && int.Parse(item[pkFiled].ToString()) != parentId)
                    {
                        continue;
                    }
                    else
                    {
                        //如果指定了只显示指定根节点以及它的子节点，则将当前节点ID加入列表
                        if (parentId > 0)
                        {
                            parentIDList += item[pkFiled].ToString() + ",";
                        }
                        //添加一行记录
                        tidyUpdata.ImportRow(item);
                        continue;
                    }
                }

                //如果指定了只显示指定根节点以及它的子节点，且当前父ID不存在父ID列表中，则终止本次循环
                if (parentId > 0 && parentIDList.IndexOf("," + pid + ",") < 0)
                {
                    continue;
                }
                //将当前ID加入列表中
                if (parentId > 0)
                {
                    parentIDList += item[pkFiled].ToString() + ",";
                }

                //寻找父id的位置
                DataRow pdrow = tidyUpdata.Rows.Find(pid);
                //获取父ID所在行索引号
                int index = tidyUpdata.Rows.IndexOf(pdrow);

                int _pid = 0;
                //查找下一个位置的父ID与当前行的父ID是否一样，是的话将插入行向下移动
                do
                {
                    //索引号增加
                    index++;
                    if (index < tidyUpdata.Rows.Count)
                    {
                        try
                        {
                            //获取下一行的父ID值
                            _pid = ConvertHelper.Cint0(tidyUpdata.Rows[index][parentIdFiled]);
                        }
                        catch (Exception)
                        {
                            _pid = 0;
                        }
                    }
                    else
                    {
                        _pid = 0;
                    }
                }
                //如果下一行的父ID值与当前要插入的ID值一样，则循环继续
                while (pid != 0 && pid == _pid);

                //当前行创建新行
                DataRow CurrentRow = tidyUpdata.NewRow();
                CurrentRow.ItemArray = item.ItemArray;

                //插入新行
                tidyUpdata.Rows.InsertAt(CurrentRow, index);
            }


            return tidyUpdata;

        }

        /// <summary>整理dataTable数据，以便于在有层次感的数据容器中使用
        /// </summary>
        /// <param name="dtable">DataTable数据源</param>
        /// <param name="pkFiled">主键ID列名</param>
        /// <param name="parentIDFiled">父级ID列名</param>
        /// <returns></returns>
        public static DataSet DataSetTidyUp(DataTable dtable, string pkFiled, string parentIDFiled)
        {

            DataSet dset = new DataSet();
            DataTable dt = DataTableTidyUp(dtable, pkFiled, parentIDFiled);
            dset.Tables.Add(dt);
            return dset;

        }
        #endregion

        #region dataTable 排序
        /// <summary>整理dataTable数据，以便于在有层次感的数据容器中使用，</summary>
        /// <param name="dtable">DataTable数据源</param>
        /// <param name="pkFiled">主键ID列名</param>
        /// <param name="parentIdFiled">父级ID列名</param>
        /// <param name="sortName">ParentID asc,SortId asc</param>
        /// <returns></returns>
        public static DataTable DataTableTreeSort(DataTable dtable, string pkFiled, string parentIdFiled = "ParentId", string sortName = "ParentId asc,SortId asc")
        {
            //判断当前内存表中是否存在指定的主键列
            if (!dtable.Columns.Contains(pkFiled) || !dtable.Columns.Contains(parentIdFiled))
            {
                //不存在指定的主键列
                return dtable;
            }

            //设定主键列
            dtable.PrimaryKey = new DataColumn[] { dtable.Columns[pkFiled] };

            //---------------------------------------------
            //先排序
            DataRow[] rows = dtable.Select("", sortName);
            DataTable tmp = dtable.Clone();
            tmp.Rows.Clear();
            foreach (DataRow row in rows)
            {
                tmp.ImportRow(row);
            }
            dtable = tmp;
            //---------------------------------------------
            //克隆内存表中的结构与约束
            DataTable dt = dtable.Clone();//克隆表结构
            dt.Rows.Clear();
            int ti = dtable.Rows.Count;
            int tj = ti;
            int dtIndex = 0;
            string pid = "";

            for (int i = 0; i < ti; i++)
            {
                DataRow rs = dtable.Rows[i];

                if (rs[parentIdFiled].ToString() != pid)
                {
                    pid = rs[parentIdFiled].ToString();
                    dtIndex = 0;
                }

                if (rs[parentIdFiled].ToString() == "0")
                {
                    dt.ImportRow(rs);
                }
                else
                {
                    if (dtIndex > 0)
                    {
                        if (dtIndex < ti - 1)
                        {
                            DataRow dr2 = dt.NewRow();
                            dr2.ItemArray = rs.ItemArray;
                            dtIndex++;
                            dt.Rows.InsertAt(dr2, dtIndex);
                        }
                    }
                    else
                    {
                        DataRow dr1 = dt.Rows.Find(rs[parentIdFiled].ToString());
                        if (dr1 != null)
                        {
                            dtIndex = dt.Rows.IndexOf(dr1);

                            if (dtIndex < ti - 1)
                            {
                                DataRow dr2 = dt.NewRow();
                                dr2.ItemArray = rs.ItemArray;
                                dtIndex++;
                                dt.Rows.InsertAt(dr2, dtIndex);
                            }
                        }
                    }
                }
            }

            return dt;
        }


        #endregion

        #region 生成多选框 for Array

        /// <summary>根据idList，输出复选框 CheckBox</summary>
        /// <param name="name">复选框指定名称</param>
        /// <param name="arr">string[]</param>
        /// <param name="idList"></param>
        /// <param name="sFlag">后缀符号</param>
        /// <returns></returns>
        public static string Get_Html_CheckBox_Array(string name, string[,] arr, string idList, string sFlag = ", ")
        {
            if (arr == null) { return ""; }
            if (arr.GetLength(0) < 1) { return ""; }

            var sb = new StringBuilder();
            try
            {
                int ti = arr.GetLength(0);

                if (idList.Length > 0)
                {
                    idList = "," + idList + ",";
                    for (int i = 0; i < ti; i++)
                    {
                        if (arr[i, 0].Length > 0 && arr[i, 1].Length > 0)
                        {
                            if (idList.IndexOf("," + arr[i, 0] + ",") > -1)
                            {
                                sb.Append(Get_Html_CheckBox(name + "_" + arr[i, 0], name, arr[i, 0], arr[i, 1], true));
                            }
                            else
                            {
                                sb.Append(Get_Html_CheckBox(name + "_" + arr[i, 0], name, arr[i, 0], arr[i, 1], false));
                            }
                            sb.Append(sFlag);
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < ti; i++)
                    {
                        if (arr[i, 0].Length > 0 && arr[i, 1].Length > 0)
                        {
                            sb.Append(Get_Html_CheckBox(name + "_" + arr[i, 0], name, arr[i, 0], arr[i, 1], false));
                            sb.Append(sFlag);
                        }
                    }
                }
            }
            catch
            {

            }

            return sb.ToString();
        }

        /// <summary> 输出 checked 控件的html </summary>
        /// <param name="sId">checked 控件的id</param>
        /// <param name="sName">checked 控件的name</param>
        /// <param name="sValue">当前值</param>
        /// <param name="sText">显示文本</param>
        /// <param name="bSel">true=已经选择,false =没有选择</param>
        /// <returns></returns>
        public static string Get_Html_CheckBox(string sId, string sName, string sValue, string sText, bool bSel = false)
        {
            string sRet = "", sChecked = "", sCss = " class=\"txtChkBoxTxt\" ";
            if (sId == "") { sId = sName; }
            if (bSel)
            {
                sChecked = " checked=\"checked\" ";
                sCss = " class=\"txtChkBoxSel\" ";
            }

            sRet = "<span><input type=\"checkbox\" name=\"{1}\" id=\"{0}\" value=\"{2}\" {4} onclick=\"checkbox_ck(this,'{0}')\"  />"
            + "<label id=\"lbl_{0}\" for=\"{0}\" {5}>{3}</label></span>";
            return string.Format(sRet, sId, sName, sValue, sText, sChecked, sCss);
        }
        #endregion

        #region 生成多选框 for DataTable
        /// <summary>根据idList，输出复选框 CheckBox</summary>
        /// <param name="sIdList">当前选择的值 id list</param>
        /// <param name="sId">checked 控件的id</param>
        /// <param name="sName">checked 控件的name</param>
        /// <param name="dt">datatable</param>
        /// <param name="sFlag"></param>
        /// <returns></returns>
        public static string Get_Html_CheckBox_DataTable(string sIdList, string sId, string sName, DataTable dt, string sFlag = ", ")
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            if (dt.Columns.Count < 2)
            {
                return "";
            }

            var sb = new StringBuilder();
            int ti = dt.Rows.Count;

            if (sIdList.Length > 0)
            {
                sIdList = "," + sIdList + ",";
                for (int i = 0; i < ti; i++)
                {
                    string s1 = dt.Rows[i][0].ToString();
                    string s2 = dt.Rows[i][1].ToString().Trim();

                    if (s1.Length <= 0 || s2.Length <= 0) continue;
                    bool bSel = (sIdList.IndexOf("," + s1 + ",", System.StringComparison.Ordinal) > -1);

                    sb.Append(Get_Html_CheckBox(sName + "_" + s1, sName, s1, s2, bSel));
                    sb.Append(sFlag);
                }
            }
            else
            {
                for (int i = 0; i < ti; i++)
                {
                    string s1 = dt.Rows[i][0].ToString();
                    string s2 = dt.Rows[i][1].ToString().Trim();

                    if (s1.Length <= 0 || s2.Length <= 0) continue;

                    sb.Append(Get_Html_CheckBox(sName + "_" + s1, sName, s1, s2, false));
                    sb.Append(sFlag);
                }
            }

            return sb.ToString();
        }
        #endregion

        #region OptionHtml
        /// <summary>
        /// '输出option 下的列表的html (注:不包括select,只输出option)
        /// Get_OptionHtml("男",string[,{id,name}])
        /// </summary>
        /// <param name="sValue">当前值</param>
        /// <param name="dt">dt</param>
        /// <param name="sFlag">后缀符号，例如：年、元等</param>
        /// <returns></returns>
        public static string Get_OptionHtml(string sValue, DataTable dt, string sFlag = "")
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            if (dt.Columns.Count < 2)
            {
                return "";
            }

            int ti = dt.Rows.Count;
            var sb = new StringBuilder();

            for (int i = 0; i < ti; i++)
            {
                string s1 = dt.Rows[i][0].ToString();
                string s2 = dt.Rows[i][1].ToString().Trim();

                if (s1.Length <= 0 || s2.Length <= 0) continue;
                string css = (s1 == sValue) ? " selected class=\"txtChkBoxSel\" " : "";

                sb.AppendFormat("<option value=\"{0}\" {3} >{1}{2}</option>", s1, s2, sFlag, css);
            }
            dt.Dispose();
            return sb.ToString();
        }
        #endregion
    }
}
