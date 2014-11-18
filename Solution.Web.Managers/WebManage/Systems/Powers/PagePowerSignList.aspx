<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagePowerSignList.aspx.cs"
    Inherits="Solution.Web.Managers.WebManage.Systems.Powers.PagePowerSignList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>页面控件权限标识管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel2" runat="server" width="870px" showborder="True"
        enableframe="false" enablecollapse="true" bodypadding="5px" layout="Column" showheader="True"
        title="页面控件权限标识管理">
        <items>
        <f:Panel ID="Panel1" Width="250px" runat="server" BodyPadding="5px" ShowBorder="False" ShowHeader="false">
            <Items>
                <f:Tree runat="server" Title="菜单树列表" ShowBorder="True" ShowHeader="True" EnableArrows="False" AutoLeafIdentification="false"
                    EnableLines="true" ID="MenuTree" OnNodeCommand="MenuTree_NodeCommand">
                </f:Tree>
            </Items>
        </f:Panel>
        <f:Panel ID="Panel4" Title="页面权限说明" Width="600px" runat="server" BodyPadding="10px" ShowBorder="False" ShowHeader="True">
            <Items>
                <f:Label ID="labelClassDesc" runat="server" Text="页面权限指的是页面中的那些按键或链接，在这里可以将这些按键与页面绑定在一起，
                未绑定的页面控件，默认用户无操作权限（按键处于禁用状态），只有绑定后才能在职位（角色）编辑时赋予其操作权限。
                在绑定页面按键权限时，左栏树列表中的页面类型菜单才可以绑定页面控件。">
                </f:Label>
                <f:HiddenField runat="server" ID="hidId" Text="0"></f:HiddenField>
            </Items>
        </f:Panel>
        <f:Panel ID="Panel9" Width="600px" Title="页面控件权限管理" runat="server" BodyPadding="10px" ShowBorder="False" ShowHeader="True" Layout="Column">
            <Items>
                <f:Panel ID="Panel5" Width="200px" ShowHeader="false" BodyPadding="10px" ShowBorder="false" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" Title="公用页面权限标识列表" EnableFrame="false" EnableCollapse="true"
                            ShowBorder="true" ShowHeader="False" AllowPaging="False" runat="server" DataKeyNames="Id" Width="180px" Height="300px">
                                <Columns>
                                    <f:BoundField Width="150px" DataField="Cname" SortField="CName" HeaderText="未绑定控件列表"  />
                                </Columns>
                            </f:Grid>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel7" Width="50px" Height="300px" ShowHeader="false" ShowBorder="false" runat="server" Layout="VBox">
                    <Items>
                        <f:Label runat="server" Height="70px"></f:Label>
                        <f:Button ID="ButtonEmpower" runat="server" Text="&nbsp; > > &nbsp;" OnClick="ButtonEmpower_Click"></f:Button>
                        <f:Label runat="server" Height="20px"></f:Label>
                        <f:Button ID="ButtonCancel" runat="server" Text="&nbsp; < < &nbsp;" OnClick="ButtonCancel_Click"></f:Button>
                        <f:Label runat="server" Height="20px"></f:Label>
                        <f:Button ID="ButtonEmpty" runat="server" Text="清 空" OnClick="ButtonEmpty_Click"></f:Button>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel6" Width="200px" ShowHeader="false" BodyPadding="10px" ShowBorder="false" runat="server">
                    <Items>
                            <f:Grid ID="Grid2" Title="公用页面权限标识列表" EnableFrame="false" EnableCollapse="true"
                            ShowBorder="true" ShowHeader="False" AllowPaging="False" runat="server" DataKeyNames="Id" Width="180px" Height="300px">
                                <Columns>
                                    <f:BoundField Width="150px" DataField="Cname" SortField="CName" HeaderText="已绑定控件列表"  />
                                </Columns>
                            </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        </items>
    </f:panel>
    </form>
</body>
</html>
