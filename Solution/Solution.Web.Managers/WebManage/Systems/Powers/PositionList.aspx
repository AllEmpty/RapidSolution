<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Powers.PositionList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>职位管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Width="870px" ShowBorder="True" EnableFrame="false"
        EnableCollapse="true" BodyPadding="5px" Layout="Column" ShowHeader="True" Title="职位管理">
        <Items>
            <f:Panel ID="Panel5" Width="250px" runat="server" BodyPadding="5px" ShowBorder="False"
                ShowHeader="false">
                <Items>
                    <f:Tree runat="server" Title="部门列表" ShowBorder="True" ShowHeader="True" EnableArrows="False"
                        AutoLeafIdentification="false" EnableLines="true" ID="BranchTree" OnNodeCommand="BranchTree_NodeCommand">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel4" Title="职位权限说明" Width="600px" runat="server" BodyPadding="10px"
                ShowBorder="False" ShowHeader="True">
                <Items>
                    <f:Label ID="labelClassDesc" runat="server" Text="职位权限管理，是将员工和部门、菜单、页面控件等权限捆绑到一块进行综合管理，
                以保护系统的安全。在设置菜单、页面访问操作权限时，要基于这样一个原则，使用户操作界面里显示最少的菜单项，
                用户不应该有的或可有可没有的项就不要给用户开这个权限，让界面简单、明了、易用。">
                    </f:Label>
                    <f:HiddenField runat="server" ID="hidId" Text="0">
                    </f:HiddenField>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel2" Width="600px" Title="职位权限管理" runat="server" BodyPadding="10px"
                ShowBorder="False" ShowHeader="True">
                <Toolbars>
                    <f:Toolbar ID="toolBar" runat="server">
                        <Items>
                            <f:Button ID="ButtonAdd" runat="server" Text="添加" Icon="Add" OnClick="ButtonAdd_Click">
                            </f:Button>
                            <f:Button ID="ButtonDelete" runat="server" Text="删除" Icon="Delete" OnClick="ButtonDelete_Click"
                                ConfirmTitle="删除提示" ConfirmText="是否删除记录？" OnClientClick="if (!F('Panel1_Panel2_Grid1').getSelectionModel().hasSelection() ) { F.alert('请选择你想要删除的记录！'); return false; } ">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Grid ID="Grid1" EnableFrame="false" EnableCollapse="true" PageSize="15" ShowBorder="true"
                        ShowHeader="False" runat="server" EnableCheckBoxSelect="True"
                        DataKeyNames="Id" EnableColumnLines="true" OnPageIndexChange="Grid1_PageIndexChange"
                        OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand">
                        <Columns>
                            <f:BoundField Width="50px" DataField="Id" SortField="Id" HeaderText="ID" TextAlign="Center" />
                            <f:BoundField DataField="Name" HeaderText="职位名称" Width="150px" />
                            <f:BoundField DataField="Branch_Name" HeaderText="归属部门" TextAlign="Center" Width="200px" />
                            <f:BoundField DataField="Branch_Code" HeaderText="部门编号" TextAlign="Center" Width="80px" />
                            <f:LinkButtonField HeaderText="操作" TextAlign="Center" ToolTip="点击修改当前记录" ColumnID="ButtonEdit"
                                CommandName="ButtonEdit" Width="70px" />
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Width="380px" Height="500px" Icon="TagBlue" Title="编辑" Hidden="True"
        EnableMaximize="True" CloseAction="HidePostBack" OnClose="Window1_Close" EnableCollapse="true"
        runat="server" EnableResize="true" BodyPadding="5px" EnableFrame="True" IFrameUrl="about:blank"
        EnableIFrame="true" EnableClose="true" IsModal="True" EnableConfirmOnClose="True">
    </f:Window>
    </form>
</body>
</html>
