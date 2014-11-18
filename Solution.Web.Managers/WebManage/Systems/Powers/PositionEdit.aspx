<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionEdit.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Powers.PositionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>职位编辑</title>
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
                            <f:TextBox runat="server" Label="职位名称" ID="txtName" ShowRedStar="true" Width="250px"></f:TextBox>
                            <f:Label runat="server" ID="labBranchName" Label="部门名称"></f:Label>
                            <f:Tree runat="server" Title="菜单（页面）树状图" ShowBorder="True" ShowHeader="True" EnableArrows="true" AutoLeafIdentification="false"
                                Width="300px" EnableLines="true" ID="MenuTree" OnNodeCheck="MenuTree_NodeCheck">
                            </f:Tree>
                            <f:HiddenField runat="server" ID="hidBranchId" Text="0"></f:HiddenField>
                            <f:HiddenField runat="server" ID="hidPositionId" Text="0"></f:HiddenField>
                        </Items>
                    </f:SimpleForm>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
