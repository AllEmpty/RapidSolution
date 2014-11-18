<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuInfoEdit.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Powers.MenuInfoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜单编辑</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="10px" EnableCollapse="True" ShowHeader="False">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSave" runat="server" Text="保存" Icon="Disk" OnClick="ButtonSave_Click"></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" runat="server" EnableFrame="false" BodyPadding="5px" EnableCollapse="True" ShowHeader="False" ShowBorder="False">
                <Items>
                    <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
                        AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">
                        <Items>
                            <f:TextBox runat="server" Label="菜单/页面名称" ID="txtName" ShowRedStar="true" Width="250px"></f:TextBox>
                            <f:DropDownList Label="菜单节点选择" AutoPostBack="true" Required="true" CompareType="String"
                                EnableSimulateTree="true" runat="server" ID="ddlParentId" Width="250px"
                                OnSelectedIndexChanged="ddlParentId_SelectedIndexChanged">
                            </f:DropDownList>
                            <f:TextBox runat="server" Label="访问路径" ID="txtUrl" ShowRedStar="true" Width="400px" EmptyText="访问Url" ></f:TextBox>
                            <f:TextBox Readonly="true" runat="server" ID="txtParent" Label="父Id" EmptyText="对应的父类Id" Width="200px" Text="0"></f:TextBox>
                            <f:TextBox runat="server" ID="txtSort" Label="排序" Width="200px" Text="0"></f:TextBox>
                            <f:RadioButtonList ID="rblIsMenu" Label="是菜单/页面" ColumnNumber="2" runat="server"
                                ShowRedStar="true" Required="true">
                                <f:RadioItem Text="菜单" Value="0" Selected="true" />
                                <f:RadioItem Text="页面" Value="1" />
                            </f:RadioButtonList>
                            <f:RadioButtonList ID="rblIsDisplay" Label="是否显示" ColumnNumber="2" runat="server"
                                ShowRedStar="true" Required="true">
                                <f:RadioItem Text="显示" Value="1" Selected="true"/>
                                <f:RadioItem Text="不显示" Value="0" />
                            </f:RadioButtonList>
                            <f:HiddenField runat="server" ID="hidId" Text="0"></f:HiddenField>
                        </Items>
                    </f:SimpleForm>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
