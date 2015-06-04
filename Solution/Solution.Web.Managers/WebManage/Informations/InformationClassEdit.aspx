<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationClassEdit.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Informations.InformationClassEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息分类编辑</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="10px" EnableCollapse="True" ShowHeader="False">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSave" runat="server" Text="保存" Icon="Disk" OnClick="ButtonSave_Click"></f:Button>
                    <f:Button ID="ButtonDeleteImage" runat="server" Text="删除图片" Icon="Delete" OnClick="ButtonDeleteImage_Click"
                        ConfirmTitle="删除提示" ConfirmText="是否删除图片？" />
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" runat="server" EnableFrame="false" BodyPadding="5px" EnableCollapse="True" ShowHeader="False" ShowBorder="False">
                <Items>
                    <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" BodyPadding="5px" runat="server" EnableCollapse="True">
                        <Items>
                            <f:TextBox runat="server" Label="名称" ID="txtName" ShowRedStar="true" Width="250px"></f:TextBox>
                            <f:DropDownList Label="信息分类节点选择" AutoPostBack="true" Required="true" CompareType="String"
                                EnableSimulateTree="true" runat="server" ID="ddlParentId" Width="250px"
                                OnSelectedIndexChanged="ddlParentId_SelectedIndexChanged">
                            </f:DropDownList>
                            <f:TextBox Readonly="true" runat="server" ID="txtParent" Label="父Id" EmptyText="对应的父类Id" Width="250px" Text="0"></f:TextBox>
                            <f:TextBox runat="server" ID="txtSort" Label="排序" Width="250px" Text="0"></f:TextBox>
                            <f:RadioButtonList ID="rblIsShow" Label="是否显示" ColumnNumber="2" runat="server"
                                ShowRedStar="true" Required="true">
                                <f:RadioItem Text="显示" Value="1" Selected="true"/>
                                <f:RadioItem Text="不显示" Value="0" />
                            </f:RadioButtonList>
                            <f:RadioButtonList ID="rblIsPage" Label="是否单页" ColumnNumber="2" runat="server"
                                ShowRedStar="true" Required="true">
                                <f:RadioItem Text="单页（没有评论）" Value="1" Selected="true" />
                                <f:RadioItem Text="不是（一般文章）" Value="0" />
                            </f:RadioButtonList>
                            <f:TextBox runat="server" ID="txtSeoTitle" Label="Seo标题" Width="400px" Text="" MaxLength="100" />
                            <f:TextBox runat="server" ID="txtSeoKey" Label="SEO关键字" Width="400px" Text="" MaxLength="100" />
                            <f:TextArea runat="server" ID="txtSeoDesc" Label="SEO说明" Width="400px" Height="60px" Text="" MaxLength="200" />
                            <f:FileUpload runat="server" ID="fuClassImg" Label="分类图" Width="400px" />
                            <f:Image runat="server" ID="imgClassImg">
                            </f:Image>
                            <f:TextArea runat="server" Label="备注" ID="txtNotes" Width="400px" Height="60px"></f:TextArea>
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
