<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvertisementList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Advertisements.AdvertisementList" %>
<%@ Import Namespace="DotNet.Utilities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>广告列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel1" runat="server" title="广告列表" enableframe="false" bodypadding="10px"
        enablecollapse="True">
        <toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonRefresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="ButtonRefresh_Click" CssClass="inline"></f:Button>
                    <f:Button ID="ButtonSearch" runat="server" Text="查询" Icon="Magnifier" OnClick="ButtonSearch_Click"></f:Button>
                    <f:Button ID="ButtonAdd" runat="server" Text="添加" Icon="Add" OnClick="ButtonAdd_Click"></f:Button>
                    <f:Button ID="ButtonSaveAutoSort" runat="server" Text="自动排序" Icon="ArrowJoin" OnClick="ButtonSaveAutoSort_Click" ConfirmTitle="自动排序提示" ConfirmText="是否对所有数据进行自动排序？"></f:Button>
                    <f:Button ID="ButtonSaveSort" runat="server" Text="保存排序" Icon="Disk" OnClick="ButtonSaveSort_Click"></f:Button>
                    <f:Button ID="ButtonDelete" runat="server" Text="删除" Icon="Delete" OnClick="ButtonDelete_Click" ConfirmTitle="删除提示" ConfirmText="是否删除记录？" 
                        OnClientClick="if (!F('Panel1_Grid1').getSelectionModel().hasSelection() ) { F.alert('请选择你想要删除的记录！'); return false; } ">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <items>
            <f:Form ID="Form6" ShowBorder="True" BodyPadding="5px" ShowHeader="False" runat="server">
                <Rows>
                    <f:FormRow ID="FormRow1" runat="server">
                        <Items>
                            <f:TextBox runat="server" ID="txtName" Label="广告名称" Width="260px" Text="" MaxLength="20"  />
                            <f:TextBox runat="server" ID="txtKeyword" Label="Key" Width="260px" Text="" MaxLength="20"  />
                            <f:DropDownList CompareType="String" Label="广告位置" EnableSimulateTree="true" runat="server" ID="dllAdvertisingPosition" Width="260px" />
                        </Items>
                    </f:FormRow>
                    <f:FormRow ID="FormRow3" runat="server">
                        <Items>
                            <f:DatePicker runat="server" Label="查询日期" ID="dpStart" DateFormatString="yyyy-M-d HH:mm:ss" Width="260px" EmptyText="查询指定日期广告" />
                            <f:DropDownList CompareType="String" Label="审批状态"
                                runat="server" ID="ddlIsDisplay" Width="260px" >
                                <f:ListItem Text="==全部==" Value="" />
                                <f:ListItem Text="已审批" Value="1" />
                                <f:ListItem Text="未审批" Value="0" />
                            </f:DropDownList>
                            <f:Label runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
            <f:Grid ID="Grid1" Title="广告列表" EnableFrame="false" EnableCollapse="true" AllowSorting="true" IsDatabasePaging="True" Height="420px" AllowPaging="True"
            PageSize="15" ShowBorder="true" ShowHeader="False" runat="server" EnableCheckBoxSelect="True" DataKeyNames="Id" EnableColumnLines="true"
            OnPageIndexChange="Grid1_PageIndexChange" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand" OnSort="Grid1_Sort">
                <Columns>
                    <f:TemplateField RenderAsRowExpander="true">
                        <ItemTemplate>
                            <div class="expander">
                                <table width="800px">
                                    <tr>
                                        <td rowspan="4" style="width: 200px;">
                                            <%# Eval("AdImg").ToString().Length > 5 ? "<a href='" + Eval("AdImg") + "' target=\"_blank\"><img src='" + DirFileHelper.GetFilePathPostfix(Eval("AdImg").ToString(), "s") + "'></a>" : ""%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;padding-top: 10px;">
                                            <strong>修改人员：</strong><%# Eval("Manager_CName")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>修改时间：</strong><%# Eval("UpdateDate")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding-top: 10px;">
                                            <strong>链接URL：</strong><a href="<%# Eval("Url")%>" target="_blank"><%# Eval("Url")%></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding-top: 10px;">
                                            <strong>备注：</strong><%# Eval("Content")%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField DataField="Id" SortField="Id" HeaderText="Id" Width="50px" />
                    <f:BoundField DataField="Name" SortField="[Name]" HeaderText="广告标题" Width="150px" />
                    <f:BoundField DataField="AdvertisingPosition_Name" SortField="AdvertisingPosition_Id" HeaderText="广告位置" Width="150px" />
                    <f:BoundField DataField="Keyword" SortField="Keyword" HeaderText="广告Key" Width="100px" />
                    <f:BoundField DataField="ShowRate" SortField="ShowRate" HeaderText="显示频率" Width="80px" />
                    <f:BoundField DataField="HitCount" SortField="HitCount" HeaderText="点击数" Width="60px" />
                    <f:BoundField DataField="StartTime" SortField="StartTime" HeaderText="开始时间" Width="130px" />
                    <f:BoundField DataField="EndTime" SortField="EndTime" HeaderText="结束时间" Width="130px" />
                    <f:TemplateField HeaderText="排序" Width="100px">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Width="50px" Text='<%# Eval("Sort") %>' AutoPostBack="false"></asp:TextBox>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:LinkButtonField ColumnID="IsDisplay" SortField="IsDisplay" HeaderText="审核" TextAlign="Center" CommandName="IsDisplay" Width="40px"  />
                    <f:LinkButtonField Width="100px" HeaderText="操作" TextAlign="Center" ToolTip="点击修改当前记录" ColumnID="ButtonEdit" CommandName="ButtonEdit" />
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text=""></f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id"></f:HiddenField>
        </items>
    </f:panel>
    <f:window id="Window1" width="680px" height="350px" icon="TagBlue" title="编辑" hidden="True"
        enablemaximize="True" closeaction="HidePostBack" onclose="Window1_Close" enablecollapse="true"
        runat="server" enableresize="true" bodypadding="5px" enableframe="True" iframeurl="about:blank"
        enableiframe="true" enableclose="true" ismodal="True" enableconfirmonclose="True">
    </f:window>
    </form>
</body>
</html>
