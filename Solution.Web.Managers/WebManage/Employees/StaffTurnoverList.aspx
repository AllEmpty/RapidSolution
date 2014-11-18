<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffTurnoverList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Employees.StaffTurnoverList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>离职员工列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Title="离职员工列表" EnableFrame="false" BodyPadding="10px"
        EnableCollapse="True">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonRefresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="ButtonRefresh_Click"
                        CssClass="inline">
                    </f:Button>
                    <f:Button ID="ButtonRecovery" runat="server" Text="复职" Icon="UserTick" OnClick="ButtonRecovery_Click"
                        ConfirmTitle="提示" ConfirmText="该员工是否复职？" OnClientClick="if (!F('Panel1_Grid1').getSelectionModel().hasSelection() ) { F.alert('请选择你要处理的记录！'); return false; } ">
                    </f:Button>
                    <f:Button ID="ButtonDelete" runat="server" Text="删除" Icon="Delete" OnClick="ButtonDelete_Click" ConfirmTitle="删除提示" ConfirmText="是否删除记录？" 
                        OnClientClick="if (!F('Panel1_Grid1').getSelectionModel().hasSelection() ) { F.alert('删除时必须选择一条将要删除的记录！'); return false; } ">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Form ID="Form6" ShowBorder="True" BodyPadding="5px" ShowHeader="False" runat="server" Width="1050px" >
                <Rows>
                    <f:FormRow ID="FormRow2" runat="server">
                        <Items>
                            <f:TextBox Label="登陆账号" ID="txtLoginName" runat="server" Width="260px" />
                            <f:TextBox Label="中文名称" ID="txtCName" runat="server" Width="260px" />
                            <f:DropDownList runat="server" ID="ddlBranch_Id" Label="所属部门" ShowRedStar="true" Required="true" Width="260px" >
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
            <f:Grid ID="Grid1" Title="离职员工列表" EnableFrame="False" EnableCollapse="true" AllowSorting="true"
                IsDatabasePaging="True" Width="1050px" PageSize="15" ShowBorder="true" ShowHeader="False" Height="420px"
                AllowPaging="true" runat="server" EnableCheckBoxSelect="True" DataKeyNames="Id"
                EnableColumnLines="true" OnPageIndexChange="Grid1_PageIndexChange" OnPreRowDataBound="Grid1_PreRowDataBound"
                OnRowCommand="Grid1_RowCommand" OnSort="Grid1_Sort">
                <Columns>
                    <f:TemplateField RenderAsRowExpander="true">
                        <ItemTemplate>
                            <div class="expander">
                                <table width="900px">
                                    <tr>
                                        <td rowspan="6" style="width: 120px; padding-top: 10px;">
                                            <%# Eval("PhotoImg").ToString().Length > 5 ? "<img width=\"100px\" height=\"100px\" src='" + DotNet.Utilities.DirFileHelper.GetFilePathPostfix(Eval("PhotoImg").ToString(), "") + "'>" : "<image width=\"100px\" height=\"75px\" src=\"../images/blank.png\"></image>"%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 250px; padding-top: 10px;">
                                            <strong>出生日期：<%# Eval("Birthday")%></strong>
                                        </td>
                                        <td style="width: 250px; padding-top: 10px;">
                                            <strong>家庭电话：</strong><%# Eval("Tel")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>学历：</strong><%# Eval("Record")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px;">
                                            <strong>联系手机：</strong><%# Eval("Mobile")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>毕业院校：</strong><%# Eval("GraduateCollege")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>Email：</strong><%# Eval("Email")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px;">
                                            <strong>专业：</strong><%# Eval("GraduateSpecialty")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>QQ：</strong><%# Eval("Qq")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>籍贯：</strong><%# Eval("NativePlace")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px;">
                                            <strong>MSN：</strong><%# Eval("Msn")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>民族：</strong><%# Eval("NationalName")%>
                                        </td>
                                        <td style="padding-top: 10px;">
                                            <strong>联系地址：</strong><%# Eval("Address")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding-top: 10px;">
                                            <strong>员工描叙：</strong><%# Eval("Content")%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField DataField="Id" HeaderText="员工ID" Width="60px" />
                    <f:BoundField DataField="CName" HeaderText="姓名(中)" Width="80px" />
                    <f:BoundField DataField="EName" HeaderText="姓名(英)" Width="80px" />
                    <f:BoundField DataField="LoginName" HeaderText="登录名(系统)" Width="100px" />
                    <f:BoundField DataField="Sex" HeaderText="性别" Width="60px" />
                    <f:BoundField DataField="Branch_Name" HeaderText="部门" Width="100px" />
                    <f:BoundField DataField="Position_Name" HeaderText="职位" Width="100px" />
                    <f:BoundField DataField="LoginTime" HeaderText="最后登录时间" Width="140px" />
                    <f:BoundField DataField="LoginIP" HeaderText="最后登录IP" Width="100px" />
                    <f:BoundField DataField="LoginCount" HeaderText="登录次数" Width="80px" />
                    <f:CheckBoxField RenderAsStaticField="true" DataField="IsEnable" HeaderText="状态" Width="50px" />
                    <f:LinkButtonField Width="100px" HeaderText="操作" ToolTip="点击修改当前记录" ColumnID="ButtonEdit"
                        CommandName="ButtonEdit" />
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text="">
            </f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id">
            </f:HiddenField>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Width="600px" Height="500px" Icon="TagBlue" Title="编辑" Hidden="True"
        EnableMaximize="True" CloseAction="HidePostBack" OnClose="Window1_Close" EnableCollapse="true"
        runat="server" EnableResize="true" BodyPadding="5px" EnableFrame="True" IFrameUrl="about:blank"
        EnableIFrame="true" EnableClose="true" IsModal="True" EnableConfirmOnClose="True">
    </f:Window>
    </form>
</body>
</html>
