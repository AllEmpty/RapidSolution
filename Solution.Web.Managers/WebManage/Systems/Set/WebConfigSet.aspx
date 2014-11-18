<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebConfigSet.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Set.WebConfigSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统配置编辑</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="10px" EnableCollapse="True" ShowHeader="False" Width="600px">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSave" runat="server" Text="保存" Icon="Disk" OnClick="ButtonSave_Click"></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" runat="server" EnableFrame="false" BodyPadding="5px" EnableCollapse="True" ShowHeader="False" ShowBorder="False" Width="600px">
                <Items>
                    <f:GroupPanel ID="GroupPanel1" runat="server" BoxConfigAlign="Center" Width="570px"
                        EnableCollapse="True" Title="网站基础信息">
                        <Items>
                            <f:TextBox ID="txtWebName" Label="网站名称" runat="server" Width="500px">
                            </f:TextBox>
                            <f:TextBox ID="txtWebDomain" Label="网站地址" runat="server" Width="500px">
                            </f:TextBox>
                            <f:TextBox ID="txtWebEmail" Label="管理员邮箱" runat="server" Width="500px">
                            </f:TextBox>
                            <f:TextBox ID="TextBox1" Label="Seo标题" runat="server" Width="500px">
                            </f:TextBox>
                            <f:TextBox ID="TextBox2" Label="Seo关键字" runat="server" Width="500px">
                            </f:TextBox>
                            <f:TextBox ID="TextBox3" Label="Seo说明" runat="server" Width="500px">
                            </f:TextBox>
                        </Items>
                    </f:GroupPanel>
                    
                    <f:GroupPanel ID="Manage" runat="server" BoxConfigAlign="Center" Width="570px"
                        EnableCollapse="True" Title="后端参数设置">
                        <Items>
                            <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" ShowBorder="false" Title="表单"  ShowHeader="false">
                                <Items>
                                    <f:RadioButtonList ID="rblIsStopWeb" runat="server" ShowRedStar="true" Required="true" Width="400px"
                                        Label="网站是否暂停">
                                        <f:RadioItem Text="正常使用" Value="0" />
                                        <f:RadioItem Text="暂停网站" Value="1" />
                                    </f:RadioButtonList>
                                    <f:TextBox ID="txtIsStopText" Label="暂停原因" runat="server" Required="true" Width="500px">
                                    </f:TextBox>
                                    <f:RadioButtonList ID="rblIsStopUserReg" runat="server" ShowRedStar="true" Required="true" Width="400px"
                                        Label="注册会员">
                                        <f:RadioItem Text="可以注册" Value="0" />
                                        <f:RadioItem Text="暂停注册" Value="1" />
                                    </f:RadioButtonList>
                                    <f:RadioButtonList ID="rblIsPostReg" runat="server" ShowRedStar="true" Required="true" Width="400px"
                                        Label="会员注册审核方式">
                                        <f:RadioItem Text="自动通过" Value="1" />
                                        <f:RadioItem Text="Email审核" Value="2" />
                                        <f:RadioItem Text="人工审核" Value="3" />
                                    </f:RadioButtonList>
                                   <f:RadioButtonList ID="rblRegIsEmailAlert" runat="server" ShowRedStar="true" Required="true" Width="400px"
                                        Label="是否发送欢迎邮件">
                                        <f:RadioItem Text="不发送" Value="0" />
                                        <f:RadioItem Text="发送" Value="1" />
                                    </f:RadioButtonList>
                                   <f:RadioButtonList ID="rblIsPostArticle" runat="server" ShowRedStar="true" Required="true" Width="400px"
                                        Label="用户评论是否需要审核">
                                        <f:RadioItem Text="不审核" Value="0" />
                                        <f:RadioItem Text="审核" Value="1" />
                                    </f:RadioButtonList>
                                    <f:NumberBox ID="txtLoginLogReserveTime" Label="登陆日志" runat="server" Width="300px">
                                    </f:NumberBox>
                                    <f:Label runat="server" Label="说明" Text="系统登陆日志保留时间，0=无限制，N（数字）= X月"></f:Label>
                                    <f:NumberBox ID="txtUseLogReserveTime" Label="操作日志" runat="server" Width="300px">
                                    </f:NumberBox>
                                    <f:Label runat="server" Label="说明" Text="系统操作日志保留时间，0=无限制，N（数字）= X月"></f:Label>
                                </Items>
                            </f:SimpleForm>
                        </Items>
                    </f:GroupPanel>
                    <f:GroupPanel ID="EmailSet" runat="server" BoxConfigAlign="Center" Width="570px"
                        Title="邮件设置" EnableCollapse="True">
                        <Items>
                            <f:SimpleForm ID="SimpleForm2" BodyPadding="5px" runat="server" ShowBorder="false" ShowHeader="false">
                                <Items>
                                    <f:TextBox runat="server" Label="SMTP服务器" ID="txtEmailSmtp" Width="300px">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="SMTP账号" ID="txtEmailUserName" Width="300px">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="SMTP密码" ID="txtEmailPassWord" Width="300px" TextMode="Password">
                                    </f:TextBox>
                                    <f:CheckBox ID="chkSendTest" ShowLabel="false" runat="server" Text="保存后发送测试邮件到管理员邮箱"  Checked="false" AutoPostBack="True">
                                    </f:CheckBox>
                                </Items>
                            </f:SimpleForm>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
