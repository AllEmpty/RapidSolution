<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>从零开始编写自己的C#框架——后端管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
        <f:Window ID="LoginWin" runat="server" Title="登录" IsModal="false" EnableClose="false" Width="350px">
        <Items>
            <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px"
                LabelWidth="60px" ShowHeader="false">
                <Items>
                    <f:TextBox ID="txtUserName" Label="用户名" runat="server" Height="32px">
                    </f:TextBox>
                    <f:TextBox ID="txtPassword" Label="密  码" TextMode="Password" runat="server" Height="32px">
                    </f:TextBox>
                    <f:TextBox ID="txtCaptcha" Label="验证码" runat="server" Height="32px">
                    </f:TextBox>
                    <f:Panel ID="Panel1" CssStyle="padding-left:65px;" ShowBorder="false" ShowHeader="false"
                        Height="50px" runat="server">
                        <Items>
                            <f:Image ID="imgCaptcha" CssStyle="float:left;" runat="server" ImageWidth="100px"
                                ImageHeight="32px">
                            </f:Image>
                            <f:LinkButton CssStyle="float:left;margin-top:8px;margin-right:100px;" ID="btnRefresh"
                                Text="看不清？" runat="server" OnClick="btnRefresh_Click">
                            </f:LinkButton>
                        </Items>
                    </f:Panel>
                </Items>
            </f:SimpleForm>
        </Items>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Left">
                <Items>
                    <f:Button ID="Button1" Text="登录" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Top"
                              runat="server" OnClick="BtnLogin_Click"/>
                    <f:Button ID="btnReset" Text="重置" Type="Reset" EnablePostBack="false" runat="server"/>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Window>
    </form>
</body>
</html>
