<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorLogList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Security.ErrorLogList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>错误日志列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel1" runat="server" title="错误日志列表" enableframe="false" bodypadding="10px"
        enablecollapse="True">
        <toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonRefresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="ButtonRefresh_Click" CssClass="inline"></f:Button>
                    <f:Button ID="ButtonSearch" runat="server" Text="查询" Icon="Magnifier" OnClick="ButtonSearch_Click"></f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <items>
            <f:Form ID="Form6" ShowBorder="True" BodyPadding="5px" ShowHeader="False" runat="server">
                <Rows>
                    <f:FormRow ID="FormRow1" runat="server">
                        <Items>
                            <f:DatePicker runat="server" Label="起始日期" ID="dpStart" DateFormatString="yyyy-M-d H:i:s" Width="260px" />
                            <f:DatePicker runat="server" Label="终止日期" ID="dpEnd" DateFormatString="yyyy-M-d H:i:s" Width="260px" />
                        </Items>
                    </f:FormRow>
                    <f:FormRow ID="FormRow2" runat="server">
                        <Items>
                            <f:TextBox Label="IP地址" ID="txtIp" runat="server" Width="260px" />
                            <f:DropDownList runat="server" ID="ddlType" Label="位置" Width="260px">
                                <f:ListItem Text="请选择" Value="-1" Selected="True" />
                                <f:ListItem Text="前台" Value="1" />
                                <f:ListItem Text="后端" Value="0" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
            <f:Grid ID="Grid1" EnableFrame="false" EnableCollapse="true" AllowSorting="true" IsDatabasePaging="True"
            PageSize="15" ShowBorder="true" ShowHeader="False" AllowPaging="true" runat="server" DataKeyNames="Id" EnableColumnLines="true"
            OnPageIndexChange="Grid1_PageIndexChange" OnSort="Grid1_Sort" OnPreRowDataBound="Grid1_PreRowDataBound">
                <Columns>
                    <f:RowNumberField Width="30px" />
                    <f:TemplateField RenderAsRowExpander="true">
                        <ItemTemplate>
                            <div class="expander">
                                <table border="0" width="800px">
                                    <tr>
                                        <td style="width:150px;">
                                            <strong>错误信息：</strong>
                                        </td>
                                        <td>
                                            <%# Eval("ErrMessage")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top:10px;">
                                            <strong>堆栈轨迹：</strong>
                                        </td>
                                        <td style="padding-top:10px;">
                                            <%# Eval("StackTrace")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top:10px;">
                                            <strong>错误产生的异常页面：</strong>
                                        </td>
                                        <td style="padding-top:10px;">
                                            <%# Eval("PageUrl")%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField DataField="ErrTime" SortField="ErrTime" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                        Width="160px" HeaderText="出错时间" />
                    <f:BoundField Width="150px" DataField="BrowserVersion" SortField="BrowserVersion" HeaderText="浏览器版本" />
                    <f:BoundField Width="120px" DataField="BrowserType" SortField="BrowserType" HeaderText="浏览器" />
                    <f:BoundField Width="120px" DataField="Ip" SortField="Ip" HeaderText="用户IP" />
                    <f:BoundField Width="80px" DataField="Type" SortField="Type" HeaderText="出错位置" />
                    <f:BoundField Width="450px" DataField="ErrSource" HeaderText="错误源" ExpandUnusedSpace="true" />
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text=""></f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id"></f:HiddenField>
        </items>
    </f:panel>
    </form>
</body>
</html>
