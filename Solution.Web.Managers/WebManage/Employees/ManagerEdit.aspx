<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerEdit.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Employees.ManagerEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>员工编辑</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="10px" EnableCollapse="True"
        ShowHeader="False">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSave" runat="server" Text="保存" Icon="Disk" OnClick="ButtonSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" runat="server" EnableFrame="false" BodyPadding="5px" EnableCollapse="True"
                ShowHeader="False" ShowBorder="False">
                <Items>
                    <f:HiddenField runat="server" ID="hidId" Text="0">
                    </f:HiddenField>
                    <f:GroupPanel ID="UserInfoAccount" runat="server" BoxConfigAlign="Center" Title="创建系统账户"
                        EnableCollapse="True">
                        <Items>
                            <f:SimpleForm ID="SimpleForm2" BodyPadding="5px" runat="server" ShowBorder="false"
                                Width="450px" ShowHeader="false">
                                <Items>
                                    <f:TextBox runat="server" Label="账号" EmptyText="当前系统所用的账户" ID="txtLoginName" ShowRedStar="true">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="账号密码" EmptyText="用户账号登录使用密码" TextMode="Password"
                                        ID="txtLoginPass">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="确认密码" EmptyText="再次输入账号登录密码" TextMode="Password"
                                        ID="txtLoginPassaAgin" CompareOperator="Equal" CompareControl="txtLoginPass"
                                        CompareMessage="两次密码不一致">
                                    </f:TextBox>
                                    <f:RadioButtonList ID="rblIsEnable" Label="账号状态" ColumnNumber="2" runat="server"
                                        Required="true">
                                        <f:RadioItem Text="启用" Value="1" Selected="true" />
                                        <f:RadioItem Text="禁用" Value="0" />
                                    </f:RadioButtonList>
                                    <f:RadioButtonList ID="rblIsMultiUser" Label="是否禁止同一帐号多人使用" ColumnNumber="2" runat="server"
                                        Required="true">
                                        <f:RadioItem Text="是" Value="0" Selected="true" />
                                        <f:RadioItem Text="否" Value="1" />
                                    </f:RadioButtonList>
                                    <f:Label runat="server" Label="说明" Text="是否禁止同一帐号多人使用，是=只能单个在线，否1=可以多人同时在线">
                                    </f:Label>
                                </Items>
                            </f:SimpleForm>
                        </Items>
                    </f:GroupPanel>
                    <f:GroupPanel ID="BasicUserInfo" runat="server" BoxConfigAlign="Center" EnableCollapse="True"
                        Title="员工基本信息">
                        <Items>
                            <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" ShowBorder="false"
                                Title="表单" Width="450px" ShowHeader="false">
                                <Items>
                                    <f:Image ID="imgPhoto" CssClass="photo" ImageWidth="200px" ImageHeight="150px" ImageUrl="../images/blank.png"
                                        runat="server">
                                    </f:Image>
                                    <f:FileUpload ID="fuSinger_AvatarPath" runat="server" Width="430px" ShowLabel="true"
                                        Label="头像上传">
                                    </f:FileUpload>
                                    <f:TextBox ID="txtCName" Label="姓名(中)" EmptyText="中文名称" runat="server" ShowRedStar="true"
                                        Required="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtEName" Label="姓名(英)" EmptyText="英文名称" runat="server">
                                    </f:TextBox>
                                    <f:RadioButtonList ID="rblSex" runat="server" Required="true" Label="性别">
                                        <f:RadioItem Text="男" Value="男" Selected="true" />
                                        <f:RadioItem Text="女" Value="女" />
                                    </f:RadioButtonList>
                                    <f:DropDownList runat="server" ID="ddlBranch_Id" Label="所属部门" ShowRedStar="true"
                                        Required="true">
                                    </f:DropDownList>
                                    <f:HiddenField ID="hidPositionId" runat="server">
                                    </f:HiddenField>
                                    <f:TextBox runat="server" Label="所属职位" ID="txtPosition" ShowRedStar="true" Readonly="True">
                                    </f:TextBox>
                                    <f:Button ID="ButtonSelectPosition" runat="server" Text="选择职位" Icon="Magnifier" EnablePostBack="False">
                                    </f:Button>
                                    <f:TextBox runat="server" Label="民族" ID="txtNationalName">
                                    </f:TextBox>
                                    <f:DatePicker runat="server" Label="出生日期" ID="dpBirthday" DateFormatString="yyyy-M-d">
                                    </f:DatePicker>
                                    <f:TextBox runat="server" Label="手机号码" ID="txtMobile">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="联系地址" ID="txtAddress">
                                    </f:TextBox>
                                </Items>
                            </f:SimpleForm>
                        </Items>
                    </f:GroupPanel>
                    <f:GroupPanel ID="GroupPanel2" runat="server" Title="完整个人信息" EnableCollapse="True">
                        <Items>
                            <f:SimpleForm ID="SimpleForm3" BodyPadding="5px" runat="server" ShowBorder="false"
                                Width="450px" ShowHeader="false">
                                <Items>
                                    <f:TextBox runat="server" Label="籍贯" ID="txtNativePlace">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="学历" ID="txtRecord">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="专业" ID="txtGraduateSpecialty">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="毕业学院" ID="txtGraduateCollege">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="家庭电话" ID="txtTel">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="QQ" ID="txtQq">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="MSN" ID="txtMsn">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="联系邮箱" ID="txtEmail">
                                    </f:TextBox>
                                    <f:HtmlEditor runat="server" Label="备注信息" ID="txtContent">
                                    </f:HtmlEditor>
                                </Items>
                            </f:SimpleForm>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="SelectWindows" Title="编辑" Hidden="true" EnableIFrame="true" runat="server" CloseAction="HidePostBack" 
        EnableMaximize="true" EnableResize="true" Target="Parent" OnClose="SelectWindows_Close" EnableClose="true" EnableConfirmOnClose="True"
        IsModal="True" Width="468px" Height="413px">
    </f:Window>
    <f:Window ID="Window1" Hidden="True"
        CloseAction="HidePostBack" OnClose="Window1_Close" EnableCollapse="true"
        runat="server" EnableResize="true" BodyPadding="5px" EnableFrame="True" IFrameUrl="about:blank"
        EnableIFrame="true" EnableClose="true" Plain="false" IsModal="True" EnableConfirmOnClose="True">
    </f:Window>
    </form>
</body>
</html>
