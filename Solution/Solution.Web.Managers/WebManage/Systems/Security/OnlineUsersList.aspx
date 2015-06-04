<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineUsersList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Security.OnlineUsersList" %>
<%@ Import Namespace="Solution.DataAccess.DataModel" %>
<%@ Import Namespace="Solution.Logic.Managers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>在线用户列表</title>
    <style type="text/css">
        .expander
        {
            padding: 5px;
        }
        .expander p
        {
            padding: 5px;
        }
        .expander strong
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel1" runat="server" title="在线用户列表" enableframe="false" bodypadding="10px"
        enablecollapse="True">
        <toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonRefresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="ButtonRefresh_Click" CssClass="inline"></f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <items>
            <f:Grid ID="Grid1" EnableFrame="false" EnableCollapse="true" AllowSorting="true" IsDatabasePaging="True" Height="420px"
            PageSize="15" ShowBorder="true" ShowHeader="False" AllowPaging="true" runat="server" DataKeyNames="Id" EnableColumnLines="true"
            OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand">
                <Columns>
                    <f:RowNumberField Width="30px" />
                    <f:TemplateField RenderAsRowExpander="true">
                        <ItemTemplate>
                            <div class="expander">
                                <p>
                                    <strong>客户端UA：</strong><%# Eval("UserAgent")%></p>
                                <p>
                                    <strong>浏览器名称：</strong><%# Eval("BrowserName")%></p>
                            </div>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField DataField="UserHashKey" HeaderText="HashKey" />
                    <f:BoundField DataField="Manager_LoginName" HeaderText="登陆帐号" />
                    <f:BoundField DataField="Manager_CName" HeaderText="用户名" />
                    <f:BoundField DataField="Sex" HeaderText="性别" />
                    <f:BoundField DataField="Branch_Name" HeaderText="部门" />
                    <f:BoundField DataField="Branch_Code" HeaderText="部门编号" />
                    <f:BoundField DataField="Position_Name" HeaderText="职位" />
                    <f:BoundField DataField="LoginTime" HeaderText="登陆时间" Width="150px" />
                    <f:TemplateField HeaderText="在线时长">
                        <ItemTemplate>
                            <f:Label ID="Label2" runat="server" Text='<%# CommonBll.LoginDuration(Eval(OnlineUsersTable.LoginTime), Eval(OnlineUsersTable.UpdateTime)) %>'>
                            </f:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField DataField="LoginIP" HeaderText="登陆Ip" />
                    <f:CheckBoxField RenderAsStaticField="true" DataField="TerminalType" HeaderText="移动设备" />
                    <f:BoundField DataField="CurrentPageTitle" HeaderText="当前所在页面" />
                    <f:LinkButtonField HeaderText="个人信息" Icon="UserBrown" ToolTip="查看当前用户详细信息" Width="80px"
                        ColumnID="ManagerColumn" CommandName="ManagerColumn" />
                    <f:LinkButtonField HeaderText="登陆日志" Icon="TableMultiple" ToolTip="查看当前用户登陆日志信息"
                        Width="80px" ColumnID="LoginLog" CommandName="LoginLog" />
                    <f:LinkButtonField HeaderText="操作日志" Icon="TableMultiple" ToolTip="查看当前用户操作日志信息"
                        Width="80px" ColumnID="UserLog" CommandName="UserLog" />
                    <f:LinkButtonField Icon="UserCross" HeaderText="踢除用户" ConfirmText="当前用户将被踢除下线，是否继续操作？"
                        Width="80px" ConfirmTitle="除提示" ToolTip="强迫当前用户下线" CommandName="GetOut" />
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text=""></f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id"></f:HiddenField>
        </items>
    </f:panel>
    <f:window id="Window1" width="750px" height="500px" icon="TagBlue" title="查看" hidden="True"
        enablemaximize="True" closeaction="HidePostBack" onclose="Window1_Close" enablecollapse="true"
        runat="server" enableresize="true" bodypadding="5px" enableframe="True" iframeurl="about:blank"
        enableiframe="true" enableclose="true" ismodal="True" enableconfirmonclose="True">
    </f:window>
    </form>
</body>
</html>
